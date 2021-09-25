/*=========================================================================================
  File Name: moduleCalendarActions.js
  Description: Calendar Module Actions
  ----------------------------------------------------------------------------------------
  Item Name: Vuexy - Vuejs, HTML & Laravel Admin Dashboard Template
  Author: Pixinvent
  Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

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
    return new Promise((resolve, reject) => {
      axios.delete(`/api/users/${userId}`)
        .then((response) => {
          commit('REMOVE_RECORD', userId)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  },
  removeRecords ({ commit }, userIds) {
    commit('SET_LOADING', true)
    return new Promise((resolve, reject) => {
      axios.delete('api/users', { userIds })
        .then((response) => {
          commit('SET_LOADING', false)
          resolve(response)
        })
        .catch((error) => { reject(error) })
    })
  }
}
