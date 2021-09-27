export default {
  isUserLoggedIn: () => {
    let isAuthenticated = false

    // get firebase current user
    // const firebaseCurrentUser = firebase.auth().currentUser

    if (localStorage.getItem('accessToken')) isAuthenticated = true
    else isAuthenticated = false

    return localStorage.getItem('userInfo') && isAuthenticated
  }
}
