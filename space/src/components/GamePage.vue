<template>
  <div class="game-page">
    <SpaceBackground />
    <AsteroidField :spaceshipZIndex="spaceshipZIndex" />
    <div class="game-container" @mousemove="moveSpaceship">
      <div class="spaceship-container" :style="{ left: mouseX + 'px', top: mouseY + 'px' }">
        <img class="spaceship" :src="spaceshipImage" alt="Spaceship" />
      </div>
    </div>
    <router-link class="back-button" :to="{ name: 'HomeView' }">Back to Home</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onUnmounted } from "vue";
import SpaceBackground from "@/components/SpaceBackground.vue";
import AsteroidField from "@/components/AsteroidField.vue";
import spaceshipImage from "@/assets/spaceship.png";

export default defineComponent({
  name: "GamePage",
  components: {
    SpaceBackground,
    AsteroidField,
  },
  data() {
    return {
      spaceshipImage,
      mouseX: 0,
      mouseY: 0,
      spaceshipZIndex: 2,
    };
  },
  mounted() {
    const moveSpaceship = (event: MouseEvent) => {
      const spaceshipContainer = this.$el.querySelector(".spaceship-container") as HTMLElement;
      const spaceshipWidth = spaceshipContainer.offsetWidth;
      const spaceshipHeight = spaceshipContainer.offsetHeight;

      const containerRect = this.$el.getBoundingClientRect();
      const containerLeft = containerRect.left;
      const containerTop = containerRect.top;

      const mouseX = event.clientX - containerLeft - spaceshipWidth / 2;
      const mouseY = event.clientY - containerTop - spaceshipHeight / 2;

      this.mouseX = mouseX;
      this.mouseY = mouseY;
    };

    window.addEventListener("mousemove", moveSpaceship);

    onUnmounted(() => {
      window.removeEventListener("mousemove", moveSpaceship);
    });
  },
});
</script>

<style scoped>
.game-page {
  position: relative;
  width: 100%;
  height: 100%;
}

.game-container {
  position: relative;
  width: 100vw;
  height: 100vh;
  background-color: transparent;
  overflow: hidden;
}

.spaceship-container {
  position: absolute;
  transform: translate(-50%, -50%);
  left: 50%;
  top: 50%;
}

.spaceship {
  width: 25px;
  height: 25px;
}

.back-button {
  position: absolute;
  bottom: 20px;
  left: 20px;
  color: #ffffff;
  text-decoration: none;
}
</style>