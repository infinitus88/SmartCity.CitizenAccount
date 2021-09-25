import axios from '@/axios.js'

export default {

  // Proceed Payment
  proceedPayment ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/payment/make-payment', payload)
        .then((response) => {

          commit('SET_CITIZENS', response.data)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  }
}