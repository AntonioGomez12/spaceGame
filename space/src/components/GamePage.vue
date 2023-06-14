<template>
  <div class="game-page">
    <Spaceship :asteroidCollision="isGameOver" @collision="handleGameOver" />
    <AsteroidField :isGameOver="isGameOver" @collision="handleGameOver" />
    <Dialog v-if="isGameOver" @close="handleDialogClose" />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
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

    function handleGameOver() {
      isGameOver.value = true;
    }

    function handleDialogClose() {
      isGameOver.value = false;
    }

    return {
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
}
</style>
