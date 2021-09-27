// axios
import axios from 'axios'

let baseURL = 'http://smartcity-citizenaccount.azurewebsites.net'

if (process.env.NODE_ENV === 'development') baseURL = 'https://localhost:44376/'

export default axios.create({
  baseURL
  // You can add your headers here
})