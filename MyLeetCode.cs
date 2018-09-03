using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMagicTools
{
    class MyLeetCode
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int val1, val2, sum, result, remainder = 0;
            ListNode node = new ListNode(0);
            ListNode Inode = node;

            while (true)
            {
                val1 = l1 == null ? 0 : l1.val;
                val2 = l2 == null ? 0 : l2.val;

                sum = val1 + val2 + remainder;
                result = sum % 10;
                remainder = sum / 10;
                
                node.next = new ListNode(result);
                node = node.next;

                if (l1 != null) l1 = l1.next;
                if (l2 != null) l2 = l2.next;

                if (l1 == null && l2 == null)
                    break;
            }

            if(remainder > 0)
            {
                node.next = new ListNode(remainder);
            }

            return Inode.next;
        }

        public static ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
        {
            int ratio = 1;
            int val1 = 0;
            while(l1 != null)
            {
                val1 += l1.val * ratio;
                ratio *= 10;
                l1 = l1.next;
            }

            ratio = 1;
            int val2 = 0;
            while (l2 != null)
            {
                val2 += l2.val * ratio;
                ratio *= 10;
                l2 = l2.next;
            }

            ListNode node = new ListNode(0);
            ListNode Inode = node;

            int result = val1 + val2;
            var resultStr = result.ToString();
            for(int i = resultStr.Length-1; i >= 0; i-- )
            {
                node.next = new ListNode(resultStr[i] - 48);
                node = node.next;
            }

            return Inode.next;
        }

        //003, 无重复字符的最长子串。abb, bba, abba, dvdf, abcabcbb, pwwkew
        public static int LengthOfLongestSubstring2(string s)
        {
            int max = 0, tmpMax = 0;
            int left = 0, right = 0;
            int maxLeft = 0, maxRight = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int index = 0;
            int count = 0;

            for(int i = 0; i < s.Length; i++)
            {
                if(!dic.TryGetValue(s[i], out index))
                {
                    dic.Add(s[i], i);
                }
                else
                {
                    tmpMax = count;
                    if (tmpMax > max) max = tmpMax;
                    dic[s[i]] = i;
                    count = i-index;
                }
            }
            if (count > max) max = count;
            return max;
        }

        //003, 无重复字符的最长子串
        public static int LengthOfLongestSubstring(string s)
        {
            int max = 0;
            int index = 0;
            int lastCollisionX = 0;
            int lastCollisionY = 0;
            int tmpMax = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!dic.TryGetValue(s[i], out index))
                {
                    dic.Add(s[i], i);
                    tmpMax = i + 1 - lastCollisionX;
                }
                else
                {
                    dic[s[i]] = i;
                    lastCollisionX = index;
                    tmpMax = i - lastCollisionY;
                    lastCollisionY = i;
                }
                if (tmpMax > max)
                    max = tmpMax;

            }
            return max;

        }

        // 004，给定两个大小为 m 和 n 的有序数组 nums1 和 nums2 。
        // 请找出这两个有序数组的中位数。要求算法的时间复杂度为 O(log (m+n)) 。
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int totalLength = nums1.Length + nums2.Length;
            int[] nums = new int[totalLength];
            bool odd = totalLength % 2 == 1;
            int halfIndex = totalLength / 2;
            bool jPlus = false;
            
            for (int i = 0, j = 0; ;)
            {
                jPlus = false;
                if (i == nums1.Length - 1)
                    jPlus = true;
                else if (nums1[i] > nums2[j])
                {
                    nums[i + j] = nums2[j];
                    jPlus = true;
                }
                else
                {
                    nums[i + j] = nums1[i];
                    jPlus = false;
                }
                

                if (i + j == halfIndex)
                    if (odd)
                        return nums[halfIndex];
                    else
                        return (float)(nums[halfIndex] + nums[halfIndex-1]) / 2;

                if (jPlus && j<nums2.Length-1)
                    j++;
                else if(i< nums1.Length-1)
                    i++;
            }
            return 0;
        }

        // 007，反转整数
        public static int Reverse(int x)
        {
            if (x == int.MinValue)
                return 0;

            string s = x.ToString();
            char[] c = s.ToCharArray();
            char[] resultChar = new char[c.Length];
            int startIndex = 0;
            if(c[0] == '-')
            {
                startIndex = 1;
                resultChar[0] = '-';
            }

            for (int i = startIndex; i < c.Length; i++)
            {
                resultChar[c.Length - i - 1 + startIndex] = c[i];
            }
            long result = long.Parse(new string(resultChar));
            if (result > int.MaxValue || result < int.MinValue)
                return 0;
            return (int)result;
        }

        // 008，字符串转整数，atoi
        //没有解决的问题，如果最终结果超过int 的表示范围怎么办。。。笨办法
        public static int MyAtoi(string str)
        {
            if (str.Length == 0)
                return 0;

            long result = 0;
            int startIndex = 0;
            bool negative = false;
            bool flag = false;

            for (int i = startIndex; i < str.Length; i++)
            {
                if (!flag && (str[i] == ' ' || str[i] == '0'))
                {
                    continue;
                }
                else if(str[i] >= 49 && str[i] <= 57)
                {
                    startIndex = i;
                    break;
                }
                else if(!flag && (str[i] == '-' || str[i] == '+'))
                {
                    flag = true;
                    if (str[i] == '-')
                        negative = true;
                }
                else
                {
                    return 0;
                }
            }

            int count = 0;
            for (int i = startIndex; i< str.Length; i++)
            {
                if(str[i] < 48 || str[i] > 57)
                {
                    break;
                }
                if (result == 0 && str[i] == '0')
                    continue;
                result = result * 10 + str[i] - 48;
                count++;
                if (count > 10 && result > 0)
                    return negative ? int.MinValue : int.MaxValue;
            }
            
            if (result > int.MaxValue)
                return negative ? int.MinValue : int.MaxValue;

            return negative? (int)-result: (int)result;
        }

        // 009，回文数
        public static bool IsPalindrome(int x)
        {
            var str = x.ToString();
            for(int i = 0; i < str.Length/2; i++)
            {
                if (str[i] != str[str.Length - i - 1])
                    return false;
            }
            return true;
        }

        public static bool IsPalindrome2(int x)
        {
            if (x < 0)
                return false;
            var str = x.ToString();
            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i] != str[str.Length - i - 1])
                    return false;
            }
            return true;
        }
    }
}
