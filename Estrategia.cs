using System;
using System.Collections.Generic;
using System.Numerics;

namespace tpfinal
{
    public class Estrategia
    {
        // 1. Shortest Job First (SJF) usando MinHeap
        public void ShortesJobFirst(List<Proceso> datos, List<Proceso> collected)
        {
            // Usamos una PriorityQueue como MinHeap para ordenar por tiempo de CPU (menor primero)
            PriorityQueue<Proceso, int> minHeap = new PriorityQueue<Proceso, int>();

            // Insertamos los procesos en la MinHeap
            foreach (var proceso in datos)
            {
                minHeap.Enqueue(proceso, proceso.tiempo);
            }

            // Extraemos los procesos en el orden de menor a mayor tiempo de uso de CPU
            while (minHeap.Count > 0)
            {
                collected.Add(minHeap.Dequeue());
            }
        }

        // 2. Preemptive Priority CPU Scheduling Algorithm (PPCSA) usando MaxHeap
        public void PreemptivePriority(List<Proceso> datos, List<Proceso> collected)
        {
            // Usamos una PriorityQueue como MaxHeap, invirtiendo el criterio de prioridad (mayor primero)
            PriorityQueue<Proceso, int> maxHeap = new PriorityQueue<Proceso, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            // Insertamos los procesos en la MaxHeap
            foreach (var proceso in datos)
            {
                maxHeap.Enqueue(proceso, proceso.prioridad);
            }

            // Extraemos los procesos en el orden de mayor a menor prioridad
            while (maxHeap.Count > 0)
            {
                collected.Add(maxHeap.Dequeue());
            }
        }

        // 3. Consulta 1: Retorna las hojas de las Heaps (últimos elementos)
        public string Consulta1(List<Proceso> datos)
        {
            PriorityQueue<Proceso, int> minHeap = new PriorityQueue<Proceso, int>();
            PriorityQueue<Proceso, int> maxHeap = new PriorityQueue<Proceso, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            foreach (var proceso in datos)
            {
                minHeap.Enqueue(proceso, proceso.tiempo);
                maxHeap.Enqueue(proceso, proceso.prioridad);
            }

            // En PriorityQueue, las hojas serán los últimos elementos insertados
            string hojasMinHeap = minHeap.Count > 0 ? minHeap.Peek().ToString() : "Sin elementos";
            string hojasMaxHeap = maxHeap.Count > 0 ? maxHeap.Peek().ToString() : "Sin elementos";

            return $"Hojas en MinHeap (SJF): {hojasMinHeap}\nHojas en MaxHeap (PPCSA): {hojasMaxHeap}";
        }

        // 4. Consulta 2: Retorna las alturas de las Heaps
        public string Consulta2(List<Proceso> datos)
        {
            int alturaMinHeap = (int)Math.Log2(datos.Count);
            int alturaMaxHeap = (int)Math.Log2(datos.Count);

            return $"Altura de la MinHeap (SJF): {alturaMinHeap}\nAltura de la MaxHeap (PPCSA): {alturaMaxHeap}";
        }

        // 5. Consulta 3: Retorna los niveles de las Heaps
        public string Consulta3(List<Proceso> datos)
        {
            PriorityQueue<Proceso, int> minHeap = new PriorityQueue<Proceso, int>();
            PriorityQueue<Proceso, int> maxHeap = new PriorityQueue<Proceso, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            foreach (var proceso in datos)
            {
                minHeap.Enqueue(proceso, proceso.tiempo);
                maxHeap.Enqueue(proceso, proceso.prioridad);
            }

            string nivelesMinHeap = GetNiveles(minHeap);
            string nivelesMaxHeap = GetNiveles(maxHeap);

            return $"Niveles de la MinHeap (SJF):\n{nivelesMinHeap}\nNiveles de la MaxHeap (PPCSA):\n{nivelesMaxHeap}";
        }

        private string GetNiveles(PriorityQueue<Proceso, int> heap)
        {
            // Esta función simula la representación por niveles.
            List<string> niveles = new List<string>();
            int nivel = 0;

            while (heap.Count > 0)
            {
                niveles.Add($"Nivel {nivel}: {heap.Dequeue().ToString()}");
                nivel++;
            }

            return string.Join("\n", niveles);
        }
    }
}
