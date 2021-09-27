export default {
  SET_LOADING (state, status) {
    state.isLoading = status
  },
  INCREASE_CITIZENS_BALANCE (state, payload) {    
    console.log(payload)
    for (const citizenId of payload.citizenIds) {
      const citizenIndex = this.state.citizen.citizens.findIndex(c => c.id === citizenId)
      this.state.citizen.citizens[citizenIndex].balance += payload.amount
      console.log(this.state.citizen.citizens[citizenIndex].balance)
    }
  } 
}