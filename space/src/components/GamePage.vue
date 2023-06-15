<template>
  <div class="game-page">
    <div class="score-container">
      <p>Score:{{ counter }}</p>
    </div>
    <div class="content-container">
      <Spaceship :asteroidCollision="isGameOver" @collision="handleGameOver" />
      <AsteroidField :isGameOver="isGameOver" @collision="handleGameOver" />
      <Dialog v-if="isGameOver" @close="handleDialogClose" />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "vue";
import Spaceship from "@/components/Spaceship.vue";
import AsteroidField from "@/components/AsteroidField.vue";
import Dialog from "@/components/Dialog.vue";

export default defineComponent({
  name: "GamePage",
  components: {
    Spaceship,
    AsteroidField,
    Dialog,
  },
  setup() {
    const isGameOver = ref(false);
    const counter = ref(0);

    function handleGameOver() {
      isGameOver.value = true;
    }

    function handleDialogClose() {
      isGameOver.value = false;
    }

    // Increase the counter by 1 every 0.10 seconds
    let intervalId = setInterval(() => {
      counter.value += 1;
    }, 100);

    // Watch for changes in isGameOver and stop the counter
    watch(isGameOver, (newValue) => {
      if (newValue) {
        clearInterval(intervalId);
      }
    });

    return {
      counter,
      isGameOver,
      handleGameOver,
      handleDialogClose,
    };
  },
});
</script>

<style scoped>
.game-page {
  position: relative;
  width: 100%;
  height: 100vh;
  background-color: #000;
  display: flex;
  flex-direction: column;
}

.score-container {
  position:absolute;
  display: flex;
  justify-content: flex-start;
  padding: 10px;
}

.content-container {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}

p {
  position: absolute;
  font-size: 40px;
  color: white;
}
</style>
