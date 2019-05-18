import { store, bus } from "@/core";
import { actions } from "./store";
import { shared } from "@/shared";

const logIn = () => {
  const { username, password, remember } = store.getState().login;

  store.dispatch(actions.setLoading(true));

  bus.triggerServer(shared.events.UI_LOGIN_SUBMIT, {
    username,
    password,
    remember
  });
};

const service = { logIn };

export { service };
