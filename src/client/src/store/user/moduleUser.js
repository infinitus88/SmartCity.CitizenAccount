import state from './moduleUserState.js'
import mutations from './moduleUserMutations.js'
import actions from './moduleUserActions.js'
import getters from './moduleUserGetters.js'

export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters
}
