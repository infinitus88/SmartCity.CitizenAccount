export default {
  SET_LOADING (state, status) {
    state.isLoading = status
  },
  SET_CITIZENS (state, citizens) {
    state.citizens = citizens
  },
  REMOVE_RECORD (state, itemId) {
    const userIndex = state.citizens.findIndex((u) => u.id === itemId)
    state.users.splice(userIndex, 1)
  }
}