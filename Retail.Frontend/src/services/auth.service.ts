/* eslint-disable @typescript-eslint/no-explicit-any */
import { UserManager, WebStorageStateStore } from "oidc-client";

export default class AuthService {
  userManager;

  constructor() {
    const settings = {
      userStore: new WebStorageStateStore({ store: window.localStorage }),
      authority: "http://localhost:7001",
      client_id: "js",
      redirect_uri: "http://localhost:5000/callback.html",
      automaticSilentRenew: true,
      silent_redirect_uri: "http://localhost:5000/silent-renew.html",
      response_type: "code",
      scope: "openid profile retail",
      post_logout_redirect_uri: "http://localhost:5000/",
      filterProtocolClaims: true,
    };

    this.userManager = new UserManager(settings);
  }

  getUser() {
    return this.userManager.getUser();
  }

  login() {
    return this.userManager.signinRedirect();
  }

  logout() {
    return this.userManager.signoutRedirect();
  }

  isAuthenticated() {
    return this.userManager.getUser().then((user: any) => {
      return user !== null && !user.expired;
    });
  }

  getAccessToken() {
    return this.userManager.getUser().then((data: any) => {
      return data.access_token;
    });
  }
}
