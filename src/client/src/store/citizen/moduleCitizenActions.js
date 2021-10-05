import axios from '@/axios.js'

export default {
  // Fetch citizens
  fetchCitizens ({ commit }) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      
      axios.get('/api/citizens')
        .then((response) => {

          commit('SET_CITIZENS', response.data)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  // Fetch citizen
  fetchCitizen (context, userId) {
    return new Promise((resolve, reject) => {
      axios.get(`/api/citizens/${userId}`)
        .then((response) => {
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  // Remove citizen
  removeRecord ({ commit }, userId) {
    return new Promise((resolve, reject) => {
      axios.delete(`/api/users/${userId}`)
        .then((response) => {
          commit('REMOVE_RECORD', userId)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  }
}