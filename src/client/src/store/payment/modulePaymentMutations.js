export default {
  SET_LOADING (state, status) {
    state.isLoading = status
  },
  INCREASE_CITIZENS_BALANCE (state, payload) {    
    for (const citizenId of payload.citizenIds) {
      const citizenIndex = this.state.citizen.citizens.findIndex(c => c.id === citizenId)
      this.state.citizen.citizens[citizenIndex].balance += payload.amount
    }
  },
  ADD_SERVICE (state, service) {
    state.services.push(service)
  },
  UPDATE_SERVICE (state, service) {
    const serviceIndex = state.services.findIndex((p) => p.id === service.id)
    Object.assign(state.services[serviceIndex], service)
  },
  REMOVE_SERVICE (state, serviceId) {
    const serviceIndex = state.services.findIndex((s) => s.id === serviceId)
    state.services.splice(serviceIndex, 1)
  },
  SET_INVOICES (state, invoices) {
    state.invoices = invoices
  },
  SET_SERVICES (state, services) {
    state.services = services
  }
}