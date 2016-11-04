namespace CFE
{
    class BinaryHeap_Min<T>
    {
        BinaryHeapWrapper<T>[] heap;
        int heapSize;

        public bool isEmpty { get { return heapSize <= 0; } }

        public BinaryHeap_Min(int size)
        {
            heap = new BinaryHeapWrapper<T>[size];
            heapSize = 0;
        }

        /**
        *<summary>
        *Returns the T with the lowest key value
        *</summary>
        */
        public T Dequeue()
        {
            if (isEmpty)
                return default(T);
            else
            {
                T retValue = heap[0].obj;
                heap[0] = heap[heapSize - 1];
                heapSize--;
                if (!isEmpty)
                    heapifyDown(0);
                return retValue;
            }

        }

        /**
        *<summary>
        *Add a T with a given key value to the priority queue
        *</summary>
        */
        public void Enqueue(T obj, int key)
        {
            if (heapSize == heap.Length)
                resizeArray();
            heapSize++;
            heap[heapSize - 1] = new BinaryHeapWrapper<T>(obj, key);
            heapifyUp(heapSize - 1);
        }

        /**
        *<summary>
        *Reorders the heap when a new object is added at the given <paramref name="nodeIndex"/>
        *</summary>
        */
        private void heapifyUp(int nodeIndex)
        {
            int parentIndex;

            //Exit the function when we reach the parent node
            //i.e. nodeIndex == 0
            if (nodeIndex != 0)
            {
                parentIndex = getParentIndex(nodeIndex);
                //If the current node is lower than its parent
                //swap them and make a recursive call
                if(heap[parentIndex].key > heap[nodeIndex].key)
                {
                    swapElements(parentIndex, nodeIndex);
                    heapifyUp(parentIndex);
                }
            }
        }

        /**
        *<summary>
        *Reorders the heap when an object is removedf from the top of the subtree starting at the given index
        *</summary>
        */
        private void heapifyDown(int nodeIndex)
        {
            int leftChildIndex, rightChildIndex, minIndex;
            leftChildIndex = getLeftChildIndex(nodeIndex);
            rightChildIndex = getRightChildIndex(nodeIndex);


            //Find whether the left child or right child is lower

            //If the right and left child index are greater than nodeIndex
            //represents a leaf and we should exit the recursive loop
            if (rightChildIndex >= heapSize)
            {
                if (leftChildIndex >= heapSize)
                    return;
                else
                    minIndex = leftChildIndex;
            }
            else
            {
                if (heap[leftChildIndex].key <= heap[rightChildIndex].key)
                    minIndex = leftChildIndex;
                else
                    minIndex = rightChildIndex;
            }
            //If the value at nodeIndex is greater than the lower of its two children
            //Swap those values so the lower value of the three is the parent node
            if(heap[nodeIndex].key > heap[minIndex].key)
            {
                swapElements(nodeIndex, minIndex);
                heapifyDown(minIndex);
            }

        }

        /**
        *<summary>
        *Swaps the position of two elements in the heap
        *</summary>
        */
        private void swapElements(int parentIndex, int childIndex)
        {
            BinaryHeapWrapper<T> temp;
            temp = heap[parentIndex];
            heap[parentIndex] = heap[childIndex];
            heap[childIndex] = temp;
        }

        /**
        *<summary>
        *Increases the size of the primary array by 1
        *</summary>
        */
        private void resizeArray()
        {
            BinaryHeapWrapper<T>[] newHeap = new BinaryHeapWrapper<T>[heapSize + 1];
            for (int i = 0; i < heapSize; i++)
            {
                newHeap[i] = heap[i];
            }
            heap = newHeap;
        }

        #region Heap->Array Index Helper Functions
        private int getLeftChildIndex(int nodeIndex)
        {
            return 2 * nodeIndex + 1;
        }

        private int getRightChildIndex(int nodeIndex)
        {
            return 2 * nodeIndex + 2;
        }

        private int getParentIndex(int nodeIndex)
        {
            return (nodeIndex - 1) / 2;
        }
        #endregion

        /**
        *<summary>
        *A Generic Wrapper to store an object with its key value
        *</summary>
        */
        private class BinaryHeapWrapper<F>
        {
            int _key;
            public int key { get { return _key; } }
            F _obj;
            public F obj { get { return _obj; } }

            public BinaryHeapWrapper(F obj, int key)
            {
                _obj = obj;
                _key = key;
            }
        }
    }


}