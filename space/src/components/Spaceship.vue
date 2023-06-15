<template>
  <div class="spaceship" :style="spaceshipStyle" ref="spaceshipRef"></div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, watch } from "vue";
import spaceshipImage from "@/assets/spaceship.png";

export default defineComponent({
  name: "Spaceship",
  props: {
    asteroidCollision: {
      type: Boolean,
      required: true,
    },
  },
  setup(props, { emit }) {
    const spaceshipRef = ref<HTMLElement | null>(null);
    const spaceshipSize = 50;
    const spaceshipStyle = ref({
      top: "calc(50% - 25px)",
      left: "calc(50% - 25px)",
    });

    const handleMouseMovement = (event: MouseEvent) => {
      const cursorX = event.clientX - spaceshipSize / 2 - 480;
      const cursorY = event.clientY - spaceshipSize / 2;

      spaceshipStyle.value.top = `${cursorY}px`;
      spaceshipStyle.value.left = `${cursorX}px`;

      const spaceshipRect = spaceshipRef.value?.getBoundingClientRect();
      const asteroids = document.querySelectorAll(".asteroid");
      const collisionThreshold = spaceshipSize / 10;

      if (spaceshipRect) {
        for (const asteroid of Array.from(asteroids)) {
          const asteroidRect = asteroid.getBoundingClientRect();

          if (
            checkCollision(spaceshipRect, asteroidRect, collisionThreshold)
          ) {
            emit("collision");
            break;
          }
        }
      }
    };

    onMounted(() => {
      window.addEventListener("mousemove", handleMouseMovement);
    });

    function checkCollision(
      rect1: ClientRect,
      rect2: ClientRect,
      threshold: number
    ): boolean {
      return (
        rect1.left - threshold < rect2.right &&
        rect1.right + threshold > rect2.left &&
        rect1.top - threshold < rect2.bottom &&
        rect1.bottom + threshold > rect2.top
      );
    }

    watch(() => props.asteroidCollision, (newValue) => {
      if (newValue) {
        // Handle collision here
        // Show dialog box or perform other actions
        console.log("Collision occurred");
      }
    });

    return {
      spaceshipStyle,
      spaceshipRef,
    };
  },
});
</script>

<style scoped>
.spaceship {
  position: absolute;
  width: 50px;
  height: 50px;
  background-image: url("@/assets/spaceship.png");
  background-size: cover;
  z-index: 2;
}
</style>
