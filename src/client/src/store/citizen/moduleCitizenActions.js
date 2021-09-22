import axios from '@/axios.js'

export default {
  // Fetch emails
  fetchCitizens ({ commit }) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      
      axios.get('/api/citizens')
        .then((response) => {

          commit('SET_CITIZENS', response.data)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },
  proceedPayment ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/payment', payload)
        .then((response) => {

          commit('SET_CITIZENS', response.data)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  }
}