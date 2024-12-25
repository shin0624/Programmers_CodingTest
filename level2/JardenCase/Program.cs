using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Solution {
    public string solution(string s) {
        if(string.IsNullOrEmpty(s) )return s;// 널이거나 비어있는 s이면 그냥 리턴

        char[] chars = s.ToCharArray(); //문자열을 문자배열로 변환
        bool newWord = true; //첫번 째 단어인지 확인하는 변수

        for(int i=0; i<chars.Length; i++) // --> 처음엔 s.Split(' ')으로 공백문자를 기준으로 나누려고 했으나, 공백문자가 여러개일 때 처리가 어렵고, 문자 단위로 처리해야 더 효율적일 듯해서 바꿈
        {
            if(chars[i] ==' ')//공백문자가 나오면
            {
                newWord = true;
                continue;//다음 문자로 넘어가기 --> 각 문자를 한 번만 순회하니까 O(N)
            }

            if(newWord)// 첫 단어일 때
            {
                if(char.IsLetter(chars[i]))//알파벳이면
                {
                    chars[i] = char.ToUpper(chars[i]);//대문자로 바꾸기
                }
            }
             else // 첫 단어가 아닐 때
            {
                if(char.IsLetter(chars[i]))//알파벳이면
                {
                    chars[i] = char.ToLower(chars[i]);//소문자로 바꾸기
                }
            }
            newWord = false;// 플래그를 false로 바꾸기
        }
        return new string(chars);//문자배열을 문자열로 변환

    }

    public static void Main(string[] args)
    {
        Solution sol = new Solution();
        string s = "3people unFollowed me";
        Console.WriteLine(sol.solution(s)); 
    }

    //jadencase = 첫 문자 대문자, 그 이외 소문자 / 첫 문자가 알파벳이 아니면 이어지는 알파벳은 소문자
    //문자열 s가 주어질 때 s를 JadenCase로 바꾼 문자열을 리턴 / s는 알파벳, 숫자, 공백문자
    //ex) "3people unFollowed me" -> "3people Unfollowed Me"
    // 가려내는 방법 :  s의 첫 문자가 알파벳인지 확인 -> 알파벳이면 대문자로 바꾸고, 그 이후 문자는 소문자로 바꾸기
    // 알파벳이 아니면 그 이후 문자는 소문자로 바꾸기
    // 공백문자가 나오면 그 다음 문자가 알파벳인지 확인
    // 알파벳인지 확인하는 방법 : char.IsLetter(s[0])
    //if(char.IsLetter[0]) -> s[0].ToUpper() 
    //else -> 다음 문자가 알파벳이면 s[0].ToLower()
    
}