import axios from '@/axios.js'

export default {

  // Fetch Invoices
  fetchInvoices ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/api/payment/invoices')
        .then((response) => {
          commit('SET_INVOICES', response.data)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },
  fetchInvoice (context, invoiceId) {
    return new Promise((resolve, reject) => {
      axios.get(`/api/payment/invoices/${invoiceId}`)
        .then((response) => {
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  // Fetch Services
  fetchServices ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/api/payment/services')
        .then((response) => {
          commit('SET_SERVICES', response.data)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },
  fetchService (context, serviceId) {
    return new Promise((resolve, reject) => {
      axios.get(`/api/payment/services/${serviceId}`)
        .then((response) => {
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  addService ({ commit }, payload) {
    return new Promise((resolve, reject) => {
      axios.post('/api/payment/add-service', payload)
        .then((response) => {
          commit('ADD_SERVICE', payload)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  updateService ({ commit }, payload) {
    const serviceId = payload.id
    delete payload.id
    return new Promise((resolve, reject) => {
      axios.post(`/api/payment/update-service/${serviceId}`, payload)
        .then((response) => {
          commit('UPDATE_SERVICE', response.data)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  removeService ({ commit }, serviceId) {
    return new Promise((resolve, reject) => {
      axios.delete(`/api/payment/delete-service/${serviceId}`)
        .then((response) => {
          commit('REMOVE_SERVICE', serviceId)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  // Proceed Payment
  proceedPayment (context, payload) {
    return new Promise((resolve, reject) => {
      axios.post('/api/payment/make-payment', payload)
        .then((response) => {
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  sendPaymentResult (context, payload) {
    return new Promise((resolve, reject) => {
      console.log(payload)
      const markPaidUrl = payload.markPaidUrl
      delete payload.markPaidUrl
      axios.post('', payload, { baseURL: markPaidUrl })
        .then((response) => {
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  },

  // Give Benefits
  giveBenefits ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/payment/give-benefits', payload)
        .then((response) => {
          delete payload.serviceName
          commit('INCREASE_CITIZENS_BALANCE', payload)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error.response.data) })
    })
  }
}