const axios = require('axios');

async function chamarAPI(n) {
  const chamadas = [];
  for (let i = 0; i < n; i++) {
    chamadas.push(executarChamada(i + 1));
  }
  await Promise.all(chamadas);
}

async function executarChamada(num) {
  const inicio = Date.now();
  try {
    const res = await axios.get('https://localhost:7050/Connector/get-stories-detailed/60');
    const fim = Date.now();
    console.log(`✅ Chamada ${num}: ${res.status} | Tempo: ${fim - inicio} ms`);
  } catch (err) {
    const fim = Date.now();
    console.error(`❌ ERRO na chamada ${num} | Tempo: ${fim - inicio} ms | Detalhe: ${err.message}`);
  }
}

chamarAPI(50); 
