<template>
    <div class="counter">
      <p v-if="!collision">Counter: {{ count }}</p>
      <h2 v-if="collision">PLAYER SCORE: {{ score }}</h2>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, onMounted, onUnmounted } from "vue";
  
  export default defineComponent({
    name: "Counter",
    setup() {
      const count = ref(0);
      const collision = ref(false);
      const score = ref(0);
      let timer: number | null = null;
  
      const startCounter = () => {
        timer = setInterval(() => {
          count.value++;
        }, 100);
      };
  
      const stopCounter = () => {
        if (timer !== null) {
          clearInterval(timer);
          collision.value = true;
          score.value = count.value;
        }
      };
  
      onMounted(() => {
        startCounter();
      });
  
      onUnmounted(() => {
        if (timer !== null) {
          clearInterval(timer);
        }
      });
  
      // Listen for collision event
      const handleCollision = () => {
        stopCounter();
      };
  
      // Register event listener
      const unregisterCollisionListener = () => {
        // Assuming there's an event bus or parent component emitting the collision event
        // Replace 'EventBus' with the appropriate event bus or parent component name
        EventBus.$on("collision", handleCollision);
      };
  
      // Unregister event listener on component unmount
      onUnmounted(() => {
        unregisterCollisionListener();
      });
  
      return {
        count,
        collision,
        score,
      };
    },
  });
  </script>
  
  <style scoped>
  .counter {
    font-size: 40px;
    color: white;
    position: fixed;
    bottom: 10px;
    left: 50%;
    transform: translateX(-50%);
    text-align: center;
  }
  
  h2 {
    font-size: 40px;
    color: white;
    position: fixed;
    top: 10px;
    left: 50%;
    transform: translateX(-50%);
    text-align: center;
  }
  </style>
  