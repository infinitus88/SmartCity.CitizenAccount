import Vue from 'vue'
import { AclInstaller, AclCreate, AclRule } from 'vue-acl'
import router from '@/router'

Vue.use(AclInstaller)

const initialRole = 'user'

// const userInfo = JSON.parse(localStorage.getItem('userInfo'))
// if (userInfo && userInfo.role) initialRole = userInfo.role

export default new AclCreate({
  initial  : initialRole,
  notfound : '/pages/not-authorized',
  router,
  acceptLocalRules : true,
  globalRules: {
    user : new AclRule('user').or('admin').generate(),
    admin  : new AclRule('admin').generate()
  }
})
