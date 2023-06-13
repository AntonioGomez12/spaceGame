<template>
    <div class="spaceship-container" :style="{ left: mouseX + 'px', top: mouseY + 'px' }">
      <img class="spaceship" :src="spaceshipImage" alt="Spaceship" />
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, onUnmounted } from "vue";
  import spaceshipImage from "@/assets/spaceship.png";
  
  export default defineComponent({
    name: "Spaceship",
    data() {
      return {
        spaceshipImage,
        mouseX: 0,
        mouseY: 0,
      };
    },
    mounted() {
      const moveSpaceship = (event: MouseEvent) => {
        const spaceshipContainer = this.$el as HTMLElement;
        const spaceshipWidth = spaceshipContainer.offsetWidth;
        const spaceshipHeight = spaceshipContainer.offsetHeight;
  
        const containerRect = this.$el.parentElement.getBoundingClientRect();
        const containerLeft = containerRect.left;
        const containerTop = containerRect.top;
  
        const mouseX = event.clientX - containerLeft - spaceshipWidth / 2 +12.5;
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
  </style>