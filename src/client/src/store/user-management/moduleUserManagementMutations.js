/*=========================================================================================
  File Name: moduleCalendarMutations.js
  Description: Calendar Module Mutations
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs Styles
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/


export default {
  SET_USERS (state, users) {
    state.users = users
  },
  REMOVE_USER (state, itemId) {
    const userIndex = state.users.findIndex((u) => u.id === itemId)
    state.users.splice(userIndex, 1)
  },
  SET_LOADING (state, status) {
    state.isLoading = status
  },
  REMOVE_USERS (state, userIds) {
    userIds.forEach((id) => {
      state.users.splice(state.users.findIndex(u => u.id === id), 1)
    })
  },
  // UPDATE_USER (state, userId, payload) {
  //   const userIndex = state.users.findIndex((user) => user.id === userId)
  //   if (userIndex !== -1) state.mails[userIndex] = payload.unreadFlag
  // },
  SET_VERIFICATION_REQUESTS (state, requests) {
    state.verificationRequests = requests
  },
  REMOVE_VERIFICATION_REQUEST (state, requestId) {
    const requestIndex = state.verificationRequests.findIndex((r) => r.id === requestId)
    state.verificationRequests.splice(requestIndex, 1)
  },
  REMOVE_VERIFICATION_REQUESTS (state, requestIds) {
    requestIds.forEach((id) => {
      state.verificationRequests.splice(state.verificationRequests.findIndex(u => u.id === id), 1)
    })
  }
}
