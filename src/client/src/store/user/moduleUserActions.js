import axios from '@/axios.js'

export default {
  updateUserInfo ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/users/update-user', payload)
        .then((response) => {
          commit('UPDATE_USER_INFO', response.data.userData, { root: true })
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    }) 
  },
  fetchVerificationStatus ({ commit }) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.get('/api/users/get-verif-status')
        .then((response) => {
          commit('SET_VERIFICATION_STATUS', response.data.status)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },
  sendVerificationRequest ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/users/create-verif-request', payload)
        .then((response) => {
          commit('SET_VERIFICATION_STATUS', 'pending')
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  }
}