import React, { ChangeEvent } from "react";
import { TState } from "./store";
import {
  Checkbox,
  Title,
  Button,
  Input,
  ShellBody,
  ShellFooter
} from "@/components";

import Logo from "@/assets/images/logo.svg";

interface TProps {
  username: TState["username"];
  password: TState["password"];
  errors: TState["errors"];
  loading: TState["loading"];
  remember: TState["remember"];
  onChangeUsername: (event: ChangeEvent<HTMLInputElement>) => void;
  onChangePassword: (event: ChangeEvent<HTMLInputElement>) => void;
  onChangeRemember: (event: ChangeEvent<HTMLInputElement>) => void;
  onClickSubmit: () => void;
  onClickGoRegister: () => void;
}

function View(props: TProps) {
  return (
    <div className="auth">
      <ShellBody data-headless>
        <div className="logo">
          <Logo />
        </div>

        <Title>Войти в аккаунт</Title>

        <div className="auth__input-block">
          <label className="auth__label">Логин</label>
          <div className="auth__input-wrapper">
            <Input
              data-padding-for-status
              value={props.username}
              onChange={props.onChangeUsername}
              placeholder="Введите свой логин"
            />
            <div
              data-error={!!props.errors.username}
              className="auth__input-status"
            />
          </div>

          <div
            data-error={!!props.errors.username}
            className="auth__input-error"
          >
            {props.errors.username}
          </div>
        </div>

        <div className="auth__input-block">
          <label className="auth__label">Пароль</label>

          <div className="auth__input-wrapper">
            <Input
              data-padding-for-status
              type="password"
              value={props.password}
              onChange={props.onChangePassword}
              placeholder="Введите свой пароль"
            />
            <div
              data-error={!!props.errors.password}
              className="auth__input-status"
            />
          </div>

          <div
            data-error={!!props.errors.password}
            className="auth__input-error"
          >
            {props.errors.password}
          </div>
        </div>

        <div className="auth__input-block auth__input-block_checkbox">
          <Checkbox checked={props.remember} onChange={props.onChangeRemember}>
            Запомнить меня
          </Checkbox>
        </div>

        <div className="auth__input-block auth__input-block_button">
          <Button disabled={props.loading} onClick={props.onClickSubmit}>
            {props.loading ? "Загрузка" : "Войти"}
          </Button>
        </div>
      </ShellBody>

      <ShellFooter TitleElement={<>Нету аккаунта?</>}>
        <Button disabled={props.loading} onClick={props.onClickGoRegister}>
          Создать
        </Button>
      </ShellFooter>
    </div>
  );
}

export { View };
