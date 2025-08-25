function mdc(a, b) {
  while (b !== 0) {
    let temp = b;
    b = a % b;
    a = temp;
  }
  return a;
}


function mmc(a, b) {
  return (a * b) / mdc(a, b);
}

// Exemplo 
const numero1 = 12;
const numero2 = 18;
console.log(`O MMC de ${numero1} e ${numero2} é: ${mmc(numero1, numero2)}`);
