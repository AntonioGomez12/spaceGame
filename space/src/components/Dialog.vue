<template>
  <div class="dialog-overlay">
    <div class="dialog">
      <h1>Game Over!</h1>
      <button class="restart-button" @click="restartGame">Restart</button>
      <button class="exit-button" @click="exitGame">Exit</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, inject } from "vue";

export default defineComponent({
  name: "Dialog",
  emits: ["close"],
  setup(_, { emit }) {
    const restartGame = (): void => {
      emit("close", "restartGame");
    };

    const exitGame = (): void => {
      emit("close", "exitGame");
    };

    return {
      restartGame,
      exitGame,
    };
  },
});
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
  background-color: white;
  padding: 20px;
  text-align: center;
}

.restart-button,
.exit-button {
  margin-top: 10px;
}
</style>

