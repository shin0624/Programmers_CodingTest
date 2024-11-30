using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int solution(int n, int k, int[] enemy) 
    {
        if(k >= enemy.Length)//무적권 개수가 현재 병사 수 이상일 때 -> 무적권만 써도 모든 랴운드 통과 가능 시
            return enemy.Length;//배열 길이 리턴

        var queue = new PriorityQueue();// 우선순위큐

        int i = 0;
        for(; i < k; ++i) // 첫 k개의 라운드는 무조건 무적권으로 처리 -> 미리 무적권을 다 써버림
            queue.Push(enemy[i]);

        // 순회하면서 최소값보다 크면 교체하고 재정렬한다.
        for(; i < enemy.Length; ++i)
        {
            int cur = enemy[i];
            if(queue.Peek() < cur)//현재 라운드의 적 수가 힙의 최소값보다 크면(즉, 내 병력이 더 적으면)
            {
                n -= queue.Pop(); // 힙의 최소값을 병력으로 지불함
                queue.Push(cur);//현재 라운드 적 수를 힙에 추가
            }
            else // 현재 라운드 적 수가 힙의 최소값 이하이면(즉, 내 병력이 더 많으면)
            {
                n -= cur;//현재 라운드 적 수를 병력으로 지불
            }

            if(n < 0)// 병사 숫자가 0이 되면 게임 종료
                return i;//해당 라운드를 반환
        }

        return enemy.Length;
    }
}

public class PriorityQueue//우선순위 큐(최소 힙) 구현
{
    List<int> heap = new List<int>();

    public void Push(int data)
    {
        heap.Add(data); // 새 데이터를 트리 맨 끝에 추가

        int now = heap.Count - 1; // 데이터를 삽입한 맨 끝 index

        while(now > 0)
        {
            int parent = (now - 1) / 2; // 부모노드
            if(heap[now] > heap[parent]) // 부모 노드와 비교하며 힙 속성 유지
                break;
            //부모보다 작은 값이면 스왑한다.
            int temp = heap[now];
            heap[now] = heap[parent];
            heap[parent] = temp;
            now = parent;
        }
    }

    public int Pop() // 루트노드 (최솟값) 반환
    {
        int ret = heap[0]; // 반환할 데이터 저장

        int lastIndex = heap.Count - 1; 
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);
        lastIndex--;

        // 아래로 스왑해가면서 정렬한다.
        int now = 0;
        while(true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now;

            if(left <= lastIndex && heap[next] > heap[left])
                next = left;

            if(right <= lastIndex && heap[next] > heap[right])
                next = right;

            if(next == now)
                break;

            int temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;

            now = next;
        }

        return ret;
    }

    public int Peek()
    {
        return heap[0];
    }


    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        int n = 7;
        int k = 3;
        int [] enemys = { 4, 2, 4, 5, 3, 3, 1};
        System.Console.WriteLine(solution.solution(n, k, enemys));
    }

    /*
        병사 수 n
        현재 라운드 i
        에너미 enemy[i] 마리
        enemy[i] 만큼 소모하여 enemy[i]마리의 적을 막을 수 있음 => 남은 병사 수 n-enemy[i]
        ex) n = 7, enemy[i]=2 => 7-2 =  5
        n < enemy[i] 이면 게임 종료

        무적권 : 병사 소모 없이 라운드 통과(최대 k번 사용)

        --> 무적권을 적절한 시기에 사용하여 최대한 많은 라운드를 진행해야 함(return : 막을수있는 최대 라운드 수)
        ex) n=7, k=3 enemy = {4, 2, 4, 5, 3, 3, 1}
            1) 1에서 무적권 안썼을 때 -> 3부터 무적권씀 -> 5번 막음
            2) 1에서 무적권 썼을 때 -> 5번 막음
    
    
    */
}