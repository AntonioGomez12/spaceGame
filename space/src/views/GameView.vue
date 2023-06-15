<template>
  <div>
    <GamePage />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import GamePage from '@/components/GamePage.vue'

export default defineComponent({
  name: 'GameView',
  components: {
    GamePage
  }
})
</script>
