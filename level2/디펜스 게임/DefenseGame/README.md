# 문제 설명

1. 준호는 요즘 디펜스 게임에 푹 빠져 있습니다. 디펜스 게임은 준호가 보유한 병사 n명으로 연속되는 적의 공격을 순서대로 막는 게임입니다. 디펜스 게임은 다음과 같은 규칙으로 진행됩니다.

2. 준호는 처음에 병사 n명을 가지고 있습니다.
3. 매 라운드마다 enemy[i]마리의 적이 등장합니다.
4. 남은 병사 중 enemy[i]명 만큼 소모하여 enemy[i]마리의 적을 막을 수 있습니다.
5. 예를 들어 남은 병사가 7명이고, 적의 수가 2마리인 경우, 현재 라운드를 막으면 7 - 2 = 5명의 병사가 남습니다.
6. 남은 병사의 수보다 현재 라운드의 적의 수가 더 많으면 게임이 종료됩니다.
7. 게임에는 무적권이라는 스킬이 있으며, 무적권을 사용하면 병사의 소모없이 한 라운드의 공격을 막을 수 있습니다.
8. 무적권은 최대 k번 사용할 수 있습니다.
9. 준호는 무적권을 적절한 시기에 사용하여 최대한 많은 라운드를 진행하고 싶습니다.

10. 준호가 처음 가지고 있는 병사의 수 n, 사용 가능한 무적권의 횟수 k, 매 라운드마다 공격해오는 적의 수가 순서대로 담긴 정수 배열 enemy가 매개변수로 주어집니다. 준호가 몇 라운드까지 막을 수 있는지 return 하도록 solution 함수를 완성해주세요.\

# 제한사항
    1 ≤ n ≤ 1,000,000,000
    1 ≤ k ≤ 500,000
    1 ≤ enemy의 길이 ≤ 1,000,000
    1 ≤ enemy[i] ≤ 1,000,000
    enemy[i]에는 i + 1 라운드에서 공격해오는 적의 수가 담겨있습니다.
    모든 라운드를 막을 수 있는 경우에는 enemy[i]의 길이를 return 해주세요.

# 풀이
 ## 프로그래머스의 .NET 버전은 7 이하이기에 PriorityQueue 클래스 사용이 불가했다. 어떻게든 우선순위 큐를 구현하지 않고 통과하려고 시도해 보았다.
1. 첫 번째 시도
```C#
public class Solution {
   public int solution(int n, int k, int[] enemy) {
        return ReCalc(n, k, enemy);
}
 public int ReCalc(int n, int k, int [] enemy)
    {
        List<int> rounds = new List<int>();//현재까지 각 라운드의 적 수를 저장

        for(int i=0; i<enemy.Length; i++)
        {
            rounds.Add(enemy[i]);//현재 라운드의 적 추가

            if(rounds.Sum() > n)//리스트에 있는 적의 총 숫자가 현재 병사 수 이상일 때
            {
                if(k==0)//무적권을 전부 다 썼을 경우
                {
                    return i;//현재 라운드까지만 방어 가능
                }

                //무적권을 쓸 수 있는 경우
                int maxRounds = rounds.Max();//지금까지 라운드 중 가장 큰 적의 수를 무적권으로 방어
                rounds.Remove(maxRounds);//방어한 라운드의 적을 제거
                k--;//무적권 감소

                if(rounds.Sum() > n){return i;}//다시 적 수 계산
            }
        }
        return enemy.Length;//모든 라운드 방어 성공 시 에너미 배열 길이 반환
    }    
```
---
각 라운드의 적 수를 리스트에 누적 저장 → 누적된 적의 총 합계 > 현재 병사 수 이면 무적권 소진 시 현재 라운드까지만 방어 가능 or 무적권 존재 시 지금까지의 라운드 중 가장 큰 적의 수를 무적권으로 방어 → **채점 결과 시간 초과 케이스 발생**

2. 두 번째 시도
```C#
public int solution(int n, int k, int[] enemy) {
    SortedDictionary<int, int> dic = new SortedDictionary<int, int>();//키를 기준으로 정렬된 순서로 데이터를 유지하는 sortedDictionary 사용
        //sortedDictionary는 이진검색트리를 기반으로 하며, 키가 항상 오름차순으로 정렬됨. 비정렬된 데이터의 삽입,제거가 빠름(OlogN)
        //key = 적 수
        //value = 해당 수의 적이 등장한 라운드 수
        
        for(int i=0; i<enemy.Length; i++)
        {
            if(dic.ContainsKey(enemy[i]))//현재 라운드의 적 수를 추가(중복 가능)
            {
                dic[enemy[i]]++;
            }
            else
            {
                dic[enemy[i]]=1;
            }

            int total = dic.Sum(x=>x.Key * x.Value);// 적 수의 총 합을 계산(해당 라운드 수 * 적의 수)
            if(total > n)//현재 병사 수보다 많다면
            {
                if(k==0)//무적권 소진 시
                {
                    return i;
                }
                
                int maxEnemy = dic.Last().Key;//가장 큰 적의 수가 있는 라운드를 무적권으로 방어
                dic[maxEnemy]--;

                if(dic[maxEnemy]==0)
                {
                    dic.Remove(maxEnemy);
                }
                k--;//무적권 감소
            }
        }
        return enemy.Length;//모든 라운드 방어 성공 시 에너미 배열 길이 반환
}
```
---
sortedDictionary를 사용하여, 적의 수를 key값으로, key 수의 적이 등장한 라운드 수를 저장하고 매 라운드마다 현재 라운드의 적 수를 추가 → (각 적의 수 * 해당 수의 라운드 수)로 총 적의 수 계산 → 병사 수 초과 시 무적권 사용 가능이면 가장 많은 적 수의 라운드를 무적권으로 처리 후 해당 라운드 수 제거 → **이것도 시간 초과 발생. ***Linq***가 가독성은 좋긴 한데 오버헤드가 있는게 큰 단점이다.**

3. 세 번째 시도
```C#
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

    public int Pop() // 루트노드 (최소값) 반환
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
```
---
**어떻게 해도 우선순위 큐를 쓰지 않으면 시간 초과를 면하지 못할 것 같았다. 결국 우선순위 큐를 구현한 후 채점했더니 바로 통과되었다.**
