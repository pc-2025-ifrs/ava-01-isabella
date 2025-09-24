function mdc(a, b) {
  // --- Validação ---
  if (arguments.length !== 2) return null;              // precisa de 2 argumentos exatos
  if (!Number.isInteger(a) || !Number.isInteger(b)) return null; // somente inteiros

  // --- Caminho feliz ---
  a = Math.abs(a);
  b = Math.abs(b);

  if (a === 0 && b === 0) return 0; // convenção: mdc(0,0) = 0

  while (b !== 0) {
    const temp = b;
    b = a % b;
    a = temp;
  }
  return a;
}

function mmc(a, b) {
  // --- Validação ---
  if (arguments.length !== 2) return null;
  if (!Number.isInteger(a) || !Number.isInteger(b)) return null;

  // --- Caminho feliz ---
  a = Math.abs(a);
  b = Math.abs(b);

  if (a === 0 || b === 0) return 0; // mmc envolvendo 0 é 0

  return (a * b) / mdc(a, b);
}

// --------------------
// Exemplos do enunciado
console.log(mmc(3, 4));      // 12
console.log(mmc(18, 131));   // 2358
console.log(mmc(-3, -4));    // 12
console.log(mmc(-5, 16));    // 80

console.log(mdc(54, 24));    // 6
console.log(mdc(-54, 24));   // 6
console.log(mdc(0, 24));     // 24
console.log(mdc(0, 0));      // 0

// Casos inválidos
console.log(mmc(3.5, 4));    // null
console.log(mmc('3', '4'));  // null
console.log(mmc([3,4]));     // null
console.log(mmc(3));         // null
console.log(mmc());          // null
console.log(mmc(3,4,5));     // null
console.log(mmc(3,4,5,6));   // null

