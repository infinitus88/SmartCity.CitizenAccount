// axios
import axios from 'axios'

const baseURL = 'http://smartcity-citizenaccount.azurewebsites.net'

export default axios.create({
  baseURL
  // You can add your headers here
  // headers: { 'Access-Control-Allow-Origin': '*' }
})
