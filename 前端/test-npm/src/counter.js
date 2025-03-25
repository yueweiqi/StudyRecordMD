export function setupCounter(element) {
    let counter = 0;
    const setCounter = (count) => {
      counter = count;
      element.innerHTML = `count2 is ${counter}`;
    };
    element.addEventListener('click', () => setCounter(counter + 1));
    setCounter(100);
  }
  