
using System;
using System.Collections.Generic;
using System.Numerics;
using tp1;

namespace tpfinal
{
    /* 
     * La clase Estrategia incluye el método BuildHeap, que construye heaps según el criterio de comparación proporcionado como parámetro. 
     * Este criterio de comparación es una expresión lambda que toma dos elementos; en este caso, dos instancias de Proceso, y las compara 
     * en función del tiempo o de la prioridad, según la expresión lambda que se utilice.
     * Faltaria implementar la consultas.
     */

    public class Estrategia
    {

        public String Consulta1(List<Proceso> datos)
        {
            string resutl = "Implementar";

            string resutl_min = "MinHeap:";
            string resutl_max = "MaxHeap: ";


            Proceso[] minHeap = BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true);

            Proceso[] maxHeap = BuildHeap(datos, (x, y) => x.prioridad.CompareTo(y.prioridad), false);

              //Implementar lo solicitado.

            return resutl_max + '\n' + resutl_min;

        }



        public String Consulta2(List<Proceso> datos)
        {
            string result = "Implementar";
            Proceso[] minHeap = BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true);
            
            int res = 0;

            //Implementar lo solicitado.
            
            return "La atura h es "+res;
        }



        public String Consulta3(List<Proceso> datos)
        {
            string result = "Implementar";

            Proceso[] minHeap = BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true);

          //Implementar lo solicitado.

            return result;
        }


        public void ShortesJobFirst(List<Proceso> datos, List<Proceso> collected)
        {

            Proceso[] minHeap = BuildHeap(datos, (x, y) => x.tiempo.CompareTo(y.tiempo), true);

            for (int posicion = 1; posicion < minHeap.Length - 1; posicion++)
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

            for (int posicion = 1; posicion < maxHeap.Length - 1; posicion++)
            {
                collected.Add(maxHeap[1]);
                maxHeap[1] = maxHeap[maxHeap.Length - 1];
                Array.Resize(ref maxHeap, maxHeap.Length - 1);
                percolate_down(maxHeap, 1, (x, y) => x.prioridad.CompareTo(y.prioridad), false);

            }
        }

        private Proceso[] BuildHeap(List<Proceso> procesos, Comparison<Proceso> comparador, bool is_min)
        {
            Proceso[] Heap = new Proceso[procesos.Count + 1];
            int indice = 0;
            foreach (Proceso entry in procesos)
            {
                Heap[++indice] = entry;
            }

            for (int posicion = indice / 2; posicion > 0; posicion--)
            {
                percolate_down(Heap, posicion, comparador, is_min);
            }

            return Heap;
        }

        private void percolate_down(Proceso[] datos, int posicion, Comparison<Proceso> comparador, bool is_min)
        {
            Proceso candidato = datos[posicion];
            bool detener_percolate = false;
            while ((2 * posicion <= (datos.Length - 1)) && !detener_percolate)
            {
                if (is_min)
                {
                    int hijo_min = 2 * posicion;
                    if (hijo_min != datos.Length - 1)
                    { //hay mas eltos, tiene hderecho
                        if (comparador.Invoke(datos[hijo_min + 1], datos[hijo_min]) < 0)
                        {
                            hijo_min++;
                        }
                    }
                    if (comparador.Invoke(candidato, datos[hijo_min]) > 0)
                    { //padre>hijo
                        datos[posicion] = datos[hijo_min];
                        posicion = hijo_min;
                    }
                    else
                    {
                        detener_percolate = true;
                    }

                }
                else
                {
                    int hijo_maximo = 2 * posicion;
                    if (hijo_maximo != datos.Length - 1)
                    { //hay mas eltos, tiene hderecho
                        if (comparador.Invoke(datos[hijo_maximo + 1], datos[hijo_maximo]) > 0)
                        {
                            hijo_maximo++;
                        }
                    }
                    if (comparador.Invoke(candidato, datos[hijo_maximo]) < 0)
                    { //padre<hijo
                        datos[posicion] = datos[hijo_maximo];
                        posicion = hijo_maximo;
                    }
                    else
                    {
                        detener_percolate = true;
                    }
                }
            }
            datos[posicion] = candidato;
        }

    }
}
