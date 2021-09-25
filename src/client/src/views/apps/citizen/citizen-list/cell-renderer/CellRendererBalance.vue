<template>
    <span v-if="params.value">${{ params.value }}</span>
    <span v-else>unknown</span>
</template>

<script>
export default {
  name: 'CellRendererBalance',
  computed: {
  }
}
</script>
