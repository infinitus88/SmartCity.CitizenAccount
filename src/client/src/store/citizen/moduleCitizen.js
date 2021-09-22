import state from './moduleCitizenState.js'
import mutations from './moduleCitizenMutations.js'
import actions from './moduleCitizenActions.js'
import getters from './moduleCitizenGetters.js'

export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters
}
