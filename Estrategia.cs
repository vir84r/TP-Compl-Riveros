using System;
using System.Collections.Generic;
using tp1;

namespace tpfinal
{
    public class Estrategia
    {
        // Consulta1: Retorna las hojas de las Heaps

        public String Consulta1(List<Proceso> datos)
        {
            List<Proceso> hojasMinHeap = GetHojas(BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true));
            List<Proceso> hojasMaxHeap = GetHojas(BuildHeap(datos, (x, y) => x.prioridad.CompareTo(y.prioridad), false));

            string resultadoMinHeap = "Hojas del MinHeap (por tiempo de ejecución):\n" + string.Join("\n", hojasMinHeap);
            string resultadoMaxHeap = "Hojas del MaxHeap (por prioridad):\n" + string.Join("\n", hojasMaxHeap);

            return resultadoMinHeap + "\n\n" + resultadoMaxHeap;
        }

        // Consulta 2: retorna las alturas de las Heaps
        
        public String Consulta2(List<Proceso> datos)
        {
            Proceso[] minHeap = BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true);
            Proceso[] maxHeap = BuildHeap(datos, (x, y) => x.prioridad.CompareTo(y.prioridad), false);

            int alturaMinHeap = GetAltura(minHeap);
            int alturaMaxHeap = GetAltura(maxHeap);

            return $"Altura del MinHeap (por tiempo de ejecución): {alturaMinHeap}\n" +
                   $"Altura del MaxHeap (por prioridad): {alturaMaxHeap}";
        }

        // Consulta 3: Retorna los datos de las Heaps con los niveles explícitos
        public String Consulta3(List<Proceso> datos)
        {
            Proceso[] minHeap = BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true);
            Proceso[] maxHeap = BuildHeap(datos, (x, y) => x.prioridad.CompareTo(y.prioridad), false);

            string nivelesMinHeap = GetNiveles(minHeap);
            string nivelesMaxHeap = GetNiveles(maxHeap);

            return $"Niveles del MinHeap (por tiempo de ejecución):\n{nivelesMinHeap}\n\n" +
                   $"Niveles del MaxHeap (por prioridad):\n{nivelesMaxHeap}";
        }

        
        public void ShortesJobFirst(List<Proceso> datos, List<Proceso> collected)
        {
            Proceso[] minHeap = BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true);

            for (int i = 1; i < minHeap.Length; i++)
            {
                collected.Add(minHeap[1]);
                minHeap[1] = minHeap[minHeap.Length - 1];
                Array.Resize(ref minHeap, minHeap.Length - 1);
                percolate_down(minHeap, 1, (x, y) => x.tiempo.CompareTo(y.tiempo), true);
            }
        }

        
        public void PreemptivePriority(List<Proceso> datos, List<Proceso> collected)
        {
            Proceso[] maxHeap = BuildHeap(datos, (x, y) => x.prioridad.CompareTo(y.prioridad), false);

            for (int i = 1; i < maxHeap.Length; i++)
            {
                collected.Add(maxHeap[1]);
                maxHeap[1] = maxHeap[maxHeap.Length - 1];
                Array.Resize(ref maxHeap, maxHeap.Length - 1);
                percolate_down(maxHeap, 1, (x, y) => x.prioridad.CompareTo(y.prioridad), false);
            }
        }

      
        private Proceso[] BuildHeap(List<Proceso> procesos, Comparison<Proceso> comparador, bool is_min)
        {
            Proceso[] heap = new Proceso[procesos.Count + 1];
            for (int i = 0; i < procesos.Count; i++)
            {
                heap[i + 1] = procesos[i];
            }

            for (int i = procesos.Count / 2; i > 0; i--)
            {
                percolate_down(heap, i, comparador, is_min);
            }

            return heap;
        }

        private void percolate_down(Proceso[] datos, int posicion, Comparison<Proceso> comparador, bool is_min)
        {
            Proceso candidato = datos[posicion];
            bool detener = false;
            while ((2 * posicion <= datos.Length - 1) && !detener)
            {
                int hijo = 2 * posicion;
                if (hijo != datos.Length - 1 && comparador.Invoke(datos[hijo + 1], datos[hijo]) * (is_min ? 1 : -1) < 0)
                {
                    hijo++;
                }
                if (comparador.Invoke(candidato, datos[hijo]) * (is_min ? 1 : -1) > 0)
                {
                    datos[posicion] = datos[hijo];
                    posicion = hijo;
                }
                else
                {
                    detener = true;
                }
            }
            datos[posicion] = candidato;
        }

        
        private List<Proceso> GetHojas(Proceso[] heap)
        {
            List<Proceso> hojas = new List<Proceso>();
            int start = heap.Length / 2;
            for (int i = start; i < heap.Length; i++)
            {
                hojas.Add(heap[i]);
            }
            return hojas;
        }

   
        private int GetAltura(Proceso[] heap)
        {
            return (int)Math.Floor(Math.Log2(heap.Length - 1));
        }

       
        private string GetNiveles(Proceso[] heap)
        {
            string resultado = "";
            int nivelActual = 0;
            int elementosEnNivel = 1;
            int contador = 0;

            for (int i = 1; i < heap.Length; i++)
            {
                if (contador == elementosEnNivel)
                {
                    nivelActual++;
                    contador = 0;
                    elementosEnNivel *= 2;
                    resultado += "\nNivel " + nivelActual + ": ";
                }
                resultado += heap[i].ToString() + " ";
                contador++;
            }

            return resultado;
        }
    }
}

