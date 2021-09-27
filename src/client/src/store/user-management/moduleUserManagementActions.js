import axios from '@/axios.js'
// import { reject, resolve } from 'core-js/fn/promise'

export default {
  // addItem({ commit }, item) {
  //   return new Promise((resolve, reject) => {
  //     axios.post("/api/data-list/products/", {item: item})
  //       .then((response) => {
  //         commit('ADD_ITEM', Object.assign(item, {id: response.data.id}))
  //         resolve(response)
  //       })
  //       .catch((error) => { reject(error) })
  //   })
  // },
  fetchUsers ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/api/users')
        .then((response) => {
          commit('SET_USERS', response.data)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },
  fetchUser (context, userId) {
    return new Promise((resolve, reject) => {
      axios.get(`/api/users/${userId}`)
        .then((response) => {
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },
  updateUser ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      const userId = payload.id
      delete payload.id
      axios.post(`/api/users/update-user/${userId}`, payload)
        .then((response) => {

          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },
  removeRecord ({ commit }, userId) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.delete(`/api/users/${userId}`)
        .then((response) => {
          commit('REMOVE_USER', userId)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },
  removeRecords ({ commit }, userIds) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('api/users/delete-users', { userIds })
        .then((response) => {
          commit('REMOVE_USERS', userIds)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

  // Verification Request
  fetchVerificationRequests ({ commit }) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.get('/api/users/verification-requests')
        .then((response) => {
          commit('SET_VERIFICATION_REQUESTS', response.data)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

  updateVerificationRequest ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/users/update-verif-request', payload)
        .then((response) => {
          commit('REMOVE_VERIFICATION_REQUEST', payload.id)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },

  updateVerificationRequests ({ commit }, payload) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.post('/api/users/update-verif-requests', payload)
        .then((response) => {
          commit('REMOVE_VERIFICATION_REQUESTS', payload.id)
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  }
}
