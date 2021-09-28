import axios from '@/axios.js'


export default {

  fetchInvoices ({ commit }) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/payment/invoices')
        .then((response) => {
          commit('SET_INVOICES', response.data)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

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
  },

  // Give Benefits
  giveBenefits ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/payment/give-benefits', payload)
        .then((response) => {
          delete payload.serviceName
          // commit('INCREASE_CITIZENS_BALANCE', payload)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  }
}