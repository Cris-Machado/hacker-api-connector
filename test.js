const axios = require('axios');

async function chamarAPI(n, times) {
  const chamadas = [];
  for (let i = 0; i < times; i++) {
    chamadas.push(executarChamada(n, i +1));
  }
  await Promise.all(chamadas);
}

async function executarChamada(num, chamada) {
  const inicio = Date.now();
  try {
    const res = await axios.get('https://localhost:7050/Connector/get-stories-detailed/60');
    const fim = Date.now();
    console.log(`✅ Chamada ${chamada}: ${res.status} | Tempo: ${fim - inicio} ms`);
  } catch (err) {
    const fim = Date.now();
    console.error(`❌ ERRO na chamada ${chamada} | Tempo: ${fim - inicio} ms | Detalhe: ${err.message}`);
  }
}

chamarAPI(300000, 100); 
