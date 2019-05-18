const { rm, cp, exec } = require("shelljs");
const find = require("find-process");
const childProcess = require("child_process");
const chokidar = require("chokidar");
const fs = require("fs");

// Не выключает ноду мгновенно, а дает возможность выполниться process.on("exit", cb)
process.stdin.resume();

var mpProcess;
var uiProcess;

const cache = {};

// Фиксим кодировку
exec("chcp 65001");

cleanServer();
buildUi();
buildClient();
buildServer();
startServer();

chokidar.watch("./server/**/*.cs").on("all", (event, path) => {
  if (canContinue(event, path)) _buildServer();
});

chokidar.watch("./client/**/*.*").on("all", (event, path) => {
  if (canContinue(event, path)) _buildClient();
});

chokidar.watch("./ui/src/**/*.*").on("all", (event, path) => {
  if (canContinue(event, path)) closeServer().then(() => startServer());
});

function buildUi() {
  uiProcess = childProcess.exec("cd ui && npx webpack --watch");
  uiProcess.unref();
  uiProcess.stdout.on("data", data => console.log(data.toString()));
  uiProcess.stderr.on("data", data => console.log(data.toString()));
}

function buildClient() {
  rm("-rf", "mp/client_packages/*");
  cp("-r", "client/javascript/*", "mp/client_packages");
  cp("-r", "client/csharp", "mp/client_packages/cs_packages");
  rm("-rf", "mp/client_packages/cs_packages/*.csproj");
  rm("-rf", "mp/client_packages/cs_packages/bin");
  rm("-rf", "mp/client_packages/cs_packages/obj");
}

function _buildClient() {
  closeServer().then(() => {
    buildClient();
    startServer();
  });
}

function buildServer() {
  exec("cd server && dotnet build && cd ..");
  cp("-r", "server/compiled/*", "mp/bridge/resources");

  cp(
    "-r",
    "server/Config/meta.xml",
    "mp/bridge/resources/netcoreapp2.0/meta.xml"
  );

  cp("-r", "server/Config/settings.xml", "mp/bridge/settings.xml");
  cp("-r", "server/Config/conf.json", "mp/conf.json");
}

function _buildServer() {
  closeServer().then(() => {
    buildServer();
    startServer();
  });
}

function startServer() {
  mpProcess = childProcess.exec(
    'cd mp && start "RAGESERVER" /B server.exe && cd ..'
  );

  mpProcess.stdout.on("data", data => console.log(data.toString()));
}

function closeServer() {
  return find("name", "server.exe", true)
    .then(r => (r.length ? r : find("name", "RAGESERVER")))
    .then(r => (r.length ? process.kill(r[0].pid) : null));
}

function cleanServer() {
  rm("-rf", "mp/bridge/resources/*");
}

function canContinue(event, path) {
  switch (event) {
    case "change":
    case "unlink":
      break;

    default:
      return false;
  }

  const content = fs.readFileSync(path).toString();

  if (cache[path] == content) return false;
  else cache[path] = content;

  return true;
}

process.on("exit", () => closeServer().then(process.exit));
process.on("SIGINT", () => closeServer().then(process.exit));
