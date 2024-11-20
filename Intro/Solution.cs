using System;
using System.Linq;
using System.Text;

public class Solution
{
   public string solution(string letter)
    {
        string[] morse = {".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."};
        char alphabet = 'a';
        string result = "";

        string[] arr = new string[26];
        for(int i=0; i<26; i++)//알파벳 배열 생성
        {
            arr[i] = alphabet++.ToString();
        }
        
        string[] letterArray = letter.Split(' ');// 입력받은 letter를 공백으로 나눔
        foreach(string x in letterArray)// 공백으로 나눈 모스부호가 저장된 letterArray의 각 원소 x 가 모스부호 배열에 들어가있다면 그 인덱스를 리턴
        {
            int idx = Array.IndexOf(morse, x);
            if(idx!=-1)
            {
                result+=arr[idx];
            }
        }
        return result;
      
    }

    static void Main(string[] args)
    {
        Solution solution = new Solution();
        string input = ".... . .-.. .-.. ---";
        Console.WriteLine(solution.solution(input)); 
    }

}