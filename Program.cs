Solution solution = new();
Console.WriteLine(string.Join(", ", solution.FullBloomFlowers(new int[][]
{
	new int[] { 1, 6 },
	new int[] { 3, 7 },
	new int[] { 9, 12 },
	new int[] { 4, 13 },
}, new int[] { 2, 3, 7, 11 })));
Console.WriteLine(string.Join(", ", solution.FullBloomFlowers(new int[][]
{
	new int[] { 1, 10 },
	new int[] { 3, 3 }
}, new int[] { 3, 3, 2 })));

public class Solution
{
	private class MinHeap
	{
		private readonly List<int> heap;

		private void Swap(int i, int j)
		{
			(heap[i], heap[j]) = (heap[j], heap[i]);
		}

		private void SiftDown(int curIdx, int endIdx)
		{
			int childOneIdx = curIdx * 2 + 1;
			while (childOneIdx <= endIdx)
			{
				int swapIdx = childOneIdx;
				int childTwoIdx = curIdx * 2 + 2;
				if (childTwoIdx <= endIdx && heap[childTwoIdx] < heap[childOneIdx])
				{
					swapIdx = childTwoIdx;
				}
				if (heap[swapIdx] < heap[curIdx])
				{
					Swap(swapIdx, curIdx);
					curIdx = swapIdx;
					childOneIdx = curIdx * 2 + 1;
				}
				else
				{
					return;
				}
			}
		}

		private void SiftUp(int curIdx)
		{
			int parentIdx = (curIdx - 1) / 2;
			while (parentIdx >= 0 && heap[parentIdx] > heap[curIdx])
			{
				Swap(curIdx, parentIdx);
				curIdx = parentIdx;
				parentIdx = (curIdx - 1) / 2;
			}
		}

		public MinHeap()
		{
			heap = new List<int>();
		}

		public int Peek()
		{
			return heap[0];
		}

		public void Push(int val)
		{
			heap.Add(val);
			SiftUp(heap.Count - 1);
		}

		public void Pop()
		{
			Swap(0, heap.Count - 1);
			heap.RemoveAt(heap.Count - 1);
			SiftDown(0, heap.Count - 1);
		}

		public int Size()
		{
			return heap.Count;
		}
	}

	public int[] FullBloomFlowers(int[][] flowers, int[] people)
	{
		int[] result = new int[people.Length];
		(int, int)[] originalPeople = new (int, int)[people.Length];
		for (int i = 0; i < people.Length; ++i)
		{
			originalPeople[i] = (people[i], i);
		}
		Array.Sort(flowers, (a, b) => a[0].CompareTo(b[0]));
		Array.Sort(originalPeople, (a, b) => a.Item1.CompareTo(b.Item1));
		MinHeap minHeap = new();
		int flowerIndex = 0;
		foreach (var (person, index) in originalPeople)
		{
			while (flowerIndex < flowers.Length && flowers[flowerIndex][0] <= person)
			{
				minHeap.Push(flowers[flowerIndex][1]);
				flowerIndex += 1;
			}
			while (minHeap.Size() > 0 && minHeap.Peek() < person)
			{
				minHeap.Pop();
			}
			result[index] = minHeap.Size();
		}
		return result;
	}
}