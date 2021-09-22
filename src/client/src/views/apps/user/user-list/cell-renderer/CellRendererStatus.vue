<template>
    <vs-chip class="ag-grid-cell-chip" :color="chipTextAndColor(params.value)[1]">
      <span>{{ chipTextAndColor(params.value)[0] }}</span>
    </vs-chip>
</template>

<script>
export default {
  name: 'CellRendererStatus',
  computed: {
    chipTextAndColor () {
      return (value) => {
        if (value === 1) return ['active', 'success']
        else if (value === 0) return ['deactivated', 'warning']
      }
    }
  }
}
</script>

<style lang="scss" scpoped>
.ag-grid-cell-chip {
  &.vs-chip-success {
    background: rgba(var(--vs-success),.15);
    color: rgba(var(--vs-success),1) !important;
    font-weight: 500;
  }
  &.vs-chip-warning {
    background: rgba(var(--vs-warning),.15);
    color: rgba(var(--vs-warning),1) !important;
    font-weight: 500;
  }
  &.vs-chip-danger {
    background: rgba(var(--vs-danger),.15);
    color: rgba(var(--vs-danger),1) !important;
    font-weight: 500;
  }
}
</style>
