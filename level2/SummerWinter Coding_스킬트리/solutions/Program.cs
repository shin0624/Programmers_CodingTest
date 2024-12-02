using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int solution(string skill, string[] skill_trees) {

        int answer = 0;//가능한 스킬트리 개수 저장

        foreach(string skill_tree in skill_trees)// 유저가 만든 skill_trees에서 하나씩 skill_tree 문자를 뽑아서 ValidSkillTree함수에 넣는다.
        {
            if(ValidSkillTree(skill, skill_tree))//만약 true를 반환하면 answer++
            {
                answer++;
            }
        }
        return answer;
    }
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

    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        string skill = "CBD";
        string[] skill_tree = {"BACDE", "CBADF", "AECB", "BDA"};
        System.Console.WriteLine(solution.solution(skill, skill_tree));
    }

    /*
        선행 스킬 순서  skill, 유저들이 만든 스킬트리 배열 skill_trees
        return 가능한 스킬트리 개수 int count
        skill = "cbd"이면 skill_trees 내에 c->b->d 순서대로 스킬이 존재해야 함

    */
}