# 문제 설명
1. **선행 스킬**이란 어떤 스킬을 배우기 전에 먼저 배워야 하는 스킬을 뜻합니다.

2. 예를 들어 선행 스킬 순서가 **스파크 → 라이트닝 볼트 → 썬더**일때, 썬더를 배우려면 먼저 라이트닝 볼트를 배워야 하고, 라이트닝 볼트를 배우려면 먼저 스파크를 배워야 합니다.

3. 위 순서에 없는 다른 스킬(힐링 등)은 순서에 상관없이 배울 수 있습니다. 따라서 스파크 → 힐링 → 라이트닝 볼트 → 썬더와 같은 스킬트리는 가능하지만, 썬더 → 스파크나 라이트닝 볼트 → 스파크 → 힐링 → 썬더와 같은 스킬트리는 불가능합니다.

4. 선행 스킬 순서 skill과 유저들이 만든 스킬트리1를 담은 배열 skill_trees가 매개변수로 주어질 때, 가능한 스킬트리 개수를 return 하는 solution 함수를 작성해주세요.

# 제한 조건
1. 스킬은 알파벳 대문자로 표기하며, 모든 문자열은 알파벳 대문자로만 이루어져 있습니다.
2. 스킬 순서와 스킬트리는 문자열로 표기합니다.
3. 예를 들어, C → B → D 라면 "CBD"로 표기합니다
4. 선행 스킬 순서 skill의 길이는 1 이상 26 이하이며, 스킬은 중복해 주어지지 않습니다.
5. skill_trees는 길이 1 이상 20 이하인 배열입니다.
6. skill_trees의 원소는 스킬을 나타내는 문자열입니다.
7. skill_trees의 원소는 길이가 2 이상 26 이하인 문자열이며, 스킬이 중복해 주어지지 않습니다.

# 풀이
---
- 유저가 만든 스킬트리 skill_trees에서 한 글자씩 뽑아낸 후 선행스킬목록 skill과 비교해야 한다.(반복 사용)

- 현재 배워야 할 스킬의 인덱스 **idx**를 0으로 초기화한다.

- skill과 일치하는지 여부를 확인하기 위해 skill에 존재하는 선행스킬 목록을 **IndexOf**로 참조하여 x와 비교한다. IndexOf는 x와 동일한 스킬이 있을 경우 그 인덱스 번호를 리턴하고, 없으면 -1을 리턴하므로, -1이 나오면 선행스킬과 무관계한 스킬로 보고 continue하고, idx와 일치하지 않는 인덱스라면 false를 리턴한다.

- idx와 동일하다면 idx를 증가시킨 후 다시 반복한다.

```C#
private bool ValidSkillTree(string skill, string skill_trees)
    {
        int idx = 0;//현재 배워야 할 스킬의 인덱스. 0부터 시작해서 반복문을 거칠 때 마다 증가.

        foreach(char x in skill_trees)
        {   
            int currentSkillPosition = skill.IndexOf(x);//현재 선행스킬의 순서에 있는 스킬인지 확인하기 위해 skill의 각 문자의 인덱스를 저장
            if(currentSkillPosition== -1) continue;//IndexOf는 인덱스 x에 해당하는 문자나 문자열이 없을 때 -1을 리턴 ==> 현재 스킬이 선행스킬 목록에 없다면 무시
            if(currentSkillPosition!=idx) return false; // 현재 찾은 스킬 위치가 배워야 할 스킬 위치와 다르면 false리턴
            /*
             //x는 유저가 만든 스킬트리 skill_trees 에서 뽑아낸 스킬
            //skill은 선행스킬순서
            //skill_trees문자열에서 순회하며 각 스킬의 순서가 맞는지, skill에 없는 스킬이 들어있는지 확인

            1. 만약 skill = "CBD", skill_trees = "CAAABD"일때
            skill.Indexof(x) 에서 x는 "C"이고  currentSkillPositon은 C의 인덱스인 0번
            currentSkillPosition = 0, idx = 0 이므로 만족. idx++ 후 다음 스킬로 넘어감.
            
            2. 선행되어야 할 스킬순서인 skill에서 x를 뽑아와서 currentSkillPosition에 넣어야 하는데, x는 유저가 만든 스킬트리.
            유저가 만든 스킬트리에는 A가 있으나, skill에는 A가 없다. 따라시 currentSkillPositonm = -1을 리턴.
            */

            idx++;//다음 스킬로 이동
        }
        return true;
    }
```
