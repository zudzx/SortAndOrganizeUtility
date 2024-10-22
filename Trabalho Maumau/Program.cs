using System;

class Program
{
    static void Main(string[] args)
    {
        bool continueRunning = true;
        //ini loop wh
        while (continueRunning)
        {
            int[] numbers = new int[10];
            Console.WriteLine("Digite 10 números:");
            //aramazena array numbers
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Número {i + 1}: ");
                numbers[i] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("\nEscolha o método de ordenação:");
            Console.WriteLine("1. Insertion Sort");
            Console.WriteLine("2. Bubble Sort");
            Console.WriteLine("3. Selection Sort");
            Console.WriteLine("4. Quick Sort");
            Console.WriteLine("5. Merge Sort");
            Console.WriteLine("6. Ordenar letras de um nome");
            Console.WriteLine("7. Ordenar lista de nomes por tamanho");
            Console.WriteLine("8. Sair");

            int choice = Convert.ToInt32(Console.ReadLine());
            //le e armazena na variavel choice

            switch (choice)
            {
                case 1:
                    InsertionSort(numbers);
                    break;
                case 2:
                    BubbleSort(numbers);
                    break;
                case 3:
                    SelectionSort(numbers);
                    break;
                case 4:
                    int quickSortComparisons = 0;
                    int quickSortSwaps = 0;
                    QuickSort(numbers, 0, numbers.Length - 1, ref quickSortComparisons, ref quickSortSwaps);
                    Console.WriteLine($"Quick Sort - Trocas: {quickSortSwaps}, Comparações: {quickSortComparisons}");
                    Console.WriteLine("Números Ordenados:");
                    PrintArray(numbers);
                    break;
                case 5:
                    int mergeSortComparisons = 0;
                    int mergeSortSwaps = 0;
                    MergeSort(numbers, 0, numbers.Length - 1, ref mergeSortComparisons, ref mergeSortSwaps);
                    Console.WriteLine($"Merge Sort - Trocas: {mergeSortSwaps}, Comparações: {mergeSortComparisons}");
                    Console.WriteLine("Números Ordenados:");
                    PrintArray(numbers);
                    break;
                case 6:
                    SortLetters();
                    break;
                case 7:
                    SortNamesByLength();
                    break;
                case 8:
                    Console.WriteLine("Saindo do programa...");
                    continueRunning = false;
                    break;
                default:
                    Console.WriteLine("Escolha inválida.");
                    break;
            }
        }
    }
    //percorre o array e insere na posição correta
    static void InsertionSort(int[] arr)
    {
        int comparisons = 0, swaps = 0;
        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
                comparisons++;
                swaps++;
            }
            arr[j + 1] = key;
            comparisons++;
        }
        Console.WriteLine($"Insertion Sort - Trocas: {swaps}, Comparações: {comparisons}");
        Console.WriteLine("Números Ordenados:");
        PrintArray(arr);
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
    //percorre varias vezes comparando pares de elementos adjascentes e troca se tiver na ordem nao correspondente
    static void BubbleSort(int[] arr)
    {
        int comparisons = 0, swaps = 0;
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swaps++;
                }
                comparisons++;
            }
        }
        Console.WriteLine($"Bubble Sort - Trocas: {swaps}, Comparações: {comparisons}");
        Console.WriteLine("Números Ordenados:");
        PrintArray(arr);
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
    //percorre muitas vzs e seleciona o menor 
    static void SelectionSort(int[] arr)
    {
        int comparisons = 0, swaps = 0;
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                    comparisons++;
                }
            }
            if (minIndex != i)
            {
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
                swaps++;
            }
        }
        Console.WriteLine($"Selection Sort - Trocas: {swaps}, Comparações: {comparisons}");
        Console.WriteLine("Números Ordenados:");
        PrintArray(arr);
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
    //divide um array em sub arrays e em seguida os combina
    static void QuickSort(int[] arr, int low, int high, ref int comparisons, ref int swaps)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high, ref comparisons, ref swaps);

            QuickSort(arr, low, pi - 1, ref comparisons, ref swaps);
            QuickSort(arr, pi + 1, high, ref comparisons, ref swaps);
        }
    }
    //para particionar o array
    static int Partition(int[] arr, int low, int high, ref int comparisons, ref int swaps)
    {
        int pivot = arr[high];
        int i = low - 1;
        int temp;

        for (int j = low; j < high; j++)
        {
            comparisons++;
            if (arr[j] < pivot)
            {
                i++;
                temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                swaps++;
            }
        }
        temp = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp;
        swaps++;

        return i + 1;
    }
    //divide um array em sub arrays e em seguida os combina e em seguid os mescla
    static void MergeSort(int[] arr, int l, int r, ref int comparisons, ref int swaps)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;

            MergeSort(arr, l, m, ref comparisons, ref swaps);
            MergeSort(arr, m + 1, r, ref comparisons, ref swaps);

            Merge(arr, l, m, r, ref comparisons, ref swaps);
        }
    }
    //mescla 2 sub array
    static void Merge(int[] arr, int l, int m, int r, ref int comparisons, ref int swaps)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; ++i)
            L[i] = arr[l + i];
        for (int j = 0; j < n2; ++j)
            R[j] = arr[m + 1 + j];

        int i1 = 0, i2 = 0;
        int k = l;

        while (i1 < n1 && i2 < n2)
        {
            comparisons++;
            if (L[i1] <= R[i2])
            {
                arr[k] = L[i1];
                i1++;
            }
            else
            {
                arr[k] = R[i2];
                i2++;
            }
            k++;
            swaps++;
        }

        while (i1 < n1)
        {
            arr[k] = L[i1];
            i1++;
            k++;
            swaps++;
        }

        while (i2 < n2)
        {
            arr[k] = R[i2];
            i2++;
            k++;
            swaps++;
        }
    }
    //converte em array de caractere
    static void SortLetters()
    {
        Console.Write("Digite um nome: ");
        string name = Console.ReadLine();
        char[] letters = name.ToCharArray();
        Array.Sort(letters);
        Console.WriteLine("Nome ordenado: " + new string(letters));
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
    //ordena o array por comprimento
    static void SortNamesByLength()
    {
        Console.WriteLine("Digite o número de nomes a serem inseridos:");
        int count = Convert.ToInt32(Console.ReadLine());
        string[] names = new string[count];

        for (int i = 0; i < count; i++)
        {
            Console.Write($"Nome {i + 1}: ");
            names[i] = Console.ReadLine();
        }

        Array.Sort(names, (x, y) => x.Length.CompareTo(y.Length));

        Console.WriteLine("\nNomes ordenados por tamanho:");
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
    //imprimi o array organizados
    static void PrintArray(int[] arr)
    {
        foreach (var num in arr)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}
