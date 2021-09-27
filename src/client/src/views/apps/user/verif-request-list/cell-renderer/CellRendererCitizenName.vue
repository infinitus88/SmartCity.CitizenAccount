<template>
  <div class="flex items-center">
    <router-link :to="url" @click.stop.prevent class="text-inherit hover:text-primary">{{ params.value }}</router-link>
  </div>
</template>

<script>
export default {
  name: 'CellRendererCitizenName',
  computed: {
    url () {
      return `/apps/user/user-view/${this.params.value.id}`

      // Below line will be for actual product
      // Currently it's commented due to demo purpose - Above url is for demo purpose
      // return "/apps/user/user-view/" + this.params.data.id
    }
  }
}
</script>
