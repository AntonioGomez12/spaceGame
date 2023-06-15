<template>
  <div class="dialog-overlay">
    <div class="dialog">
      <h1 class="game-over-title">GAME OVER</h1>
      <button class="restart-button" @click="restartGame">Play Again?</button>
      <br />
      <br />
      <button class="exit-button" @click="exitGame">Exit</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, inject } from 'vue'
import { useRouter } from 'vue-router'

export default defineComponent({
  name: 'Dialog',
  emits: ['close'],
  setup(_, { emit }) {
    const router = useRouter()

    const restartGame = (): void => {
      window.location.reload()
    }

    const exitGame = (): void => {
      router.push('/')
    }

    return {
      restartGame,
      exitGame
    }
  }
})
</script>

<style scoped>
.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 2;
}

.dialog {
  background-color: transparent;
  padding: 20px;
  text-align: center;
}

.game-over-title {
  color: red;
  font-size: 64px;
  margin-bottom: 20px;
}

.restart-button,
.exit-button {
  outline: 5px;
  outline-color: red;
  font-size: 20px;
  color: yellow;
  width: 120px;
  height: 40px;
  background-color: transparent;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  outline: none;
}
</style>
