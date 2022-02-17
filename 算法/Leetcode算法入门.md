# Leetcode算法入门

## 第 1 天 二分查找

### [704. 二分查找](https://leetcode-cn.com/problems/binary-search/)

#### 题目

给定一个 n 个元素有序的（升序）整型数组 nums 和一个目标值 target  ，写一个函数搜索 nums 中的 target，如果目标值存在返回下标，否则返回 -1。

**示例 1:**

```
输入: nums = [-1,0,3,5,9,12], target = 9
输出: 4
解释: 9 出现在 nums 中并且下标为 4
```

**示例 2:**

```
输入: nums = [-1,0,3,5,9,12], target = 2
输出: -1
解释: 2 不存在 nums 中因此返回 -1
```

**提示：**

1. 你可以假设 nums 中的所有元素是不重复的。
2. n 将在 [1, 10000]之间。
3. nums 的每个元素都将在 [-9999, 9999]之间。

#### 题解

对于有序数组，要在数组中寻找目标值，可以考虑二分法。

- 如果中间值等于目标值，则此项即为目标
- 如果中间值小于目标值，则目标值位于中间值右侧
- 如果中间值大于目标值，则目标值位于中间值左侧

每次查找都会将查找范围缩小一半，，因此二分查找的时间复杂度是*O(log n)*，其中 n*n* 是数组的长度。

二分查找的条件是查找范围不为空，即*left*≤*right*。

```C#
public class Solution
{
    public int Search(int[] nums, int target)
    {
        var low = 0;
        var high = nums.Length - 1;
        while (low <= high)
        {
            // 虽然本题不会溢出，但是为了防止溢出，不要写 var mid = (low + high) / 2
            var mid = (low - high) / 2 + low;
            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
        return -1;
    }
}
```

#### 复杂度分析

- 时间复杂度：O(log n)，其中 n 是数组的长度。
- 空间复杂度：O(1)。

### [278. 第一个错误的版本](https://leetcode-cn.com/problems/first-bad-version/)

#### 题目

你是产品经理，目前正在带领一个团队开发新的产品。不幸的是，你的产品的最新版本没有通过质量检测。由于每个版本都是基于之前的版本开发的，所以错误的版本之后的所有版本都是错的。

假设你有 n 个版本 [1, 2, ..., n]，你想找出导致之后所有版本出错的第一个错误的版本。

你可以通过调用 `bool isBadVersion(version)` 接口来判断版本号 `version` 是否在单元测试中出错。实现一个函数来查找第一个错误的版本。你应该尽量减少对调用 API 的次数。

**示例 1：**

```
输入：n = 5, bad = 4
输出：4
解释：
调用 isBadVersion(3) -> false 
调用 isBadVersion(5) -> true 
调用 isBadVersion(4) -> true
所以，4 是第一个错误的版本。
```

**示例 2：**

```
输入：n = 1, bad = 1
输出：1
```


提示：

- 1 <= bad <= n <= 2^31^ - 1

####   题解

```C#
/* The isBadVersion API is defined in the parent class VersionControl.
      bool IsBadVersion(int version); */
public class Solution : VersionControl
{
    public int FirstBadVersion(int n)
    {
        var low = 1;
        var high = n;
        while (low < high) // 循环直至区间左右端点相同
        {
            int mid = low + (high - low) / 2; // 防止计算时溢出
            if (IsBadVersion(mid))
            {
                high = mid; // 答案在区间 [low, mid] 中
            }
            else
            {
                low = mid + 1;
            }
        }
        // 此时有 low == high，区间缩为一个点，即为答案
        return low;
    }
}
```

#### 复杂度分析

- 时间复杂度：O(log n)，其中 n 是给定版本的数量。
- 空间复杂度：O(1)。我们只需要常数的空间保存若干变量。

### [35. 搜索插入位置](https://leetcode-cn.com/problems/search-insert-position/)

#### 题目

给定一个排序数组和一个目标值，在数组中找到目标值，并返回其索引。如果目标值不存在于数组中，返回它将会被按顺序插入的位置。

请必须使用时间复杂度为 O(log n) 的算法。

**示例 1:**

```
输入: nums = [1,3,5,6], target = 5
输出: 2
```

**示例 2:**

```
输入: nums = [1,3,5,6], target = 2
输出: 1
```

**示例 3:**

```
输入: nums = [1,3,5,6], target = 7
输出: 4
```

**示例 4:**

```
输入: nums = [1,3,5,6], target = 0
输出: 0
```

**示例 5:**

```
输入: nums = [1], target = 0
输出: 0
```

**提示:**

- 1 <= nums.length <= 10^4^
- -10^4^ <= nums[i] <= 10^4^
- nums 为无重复元素的升序排列数组
- -10^4^ <= target <= 10^4^

#### 题解

```C#
public class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        var low = 0;
        var high = nums.Length - 1;
        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            if (nums[mid] == target)
            {
                // 找到目标值，直接返回
                return mid;
            }
            else if (nums[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
        // 没找到目标值，考虑三种情况
        // 1. 目标比数组最小值小，返回0
        // 2. 目标比数组最大值大，返回数组长度
        // 3. 目标在数组最大值和最小值之间，如【1,2,3,4,5,7】和 6，返回 5
        // 综合以上情况，返回 low 即可
        return low;
    }
}
```

#### 复杂度分析

- 时间复杂度：O(log n)，其中 n 为数组的长度。二分查找所需的时间复杂度为 O(log n)。

- 空间复杂度：O(1)。我们只需要常数空间存放若干变量。

## 第 2 天 双指针

### [977. 有序数组的平方](https://leetcode-cn.com/problems/squares-of-a-sorted-array/)

#### 题目

给你一个按 非递减顺序 排序的整数数组 nums，返回 每个数字的平方 组成的新数组，要求也按 非递减顺序 排序。

**示例 1：**

```
输入：nums = [-4,-1,0,3,10]
输出：[0,1,9,16,100]
解释：平方后，数组变为 [16,1,0,9,100]
排序后，数组变为 [0,1,9,16,100]
```

**示例 2：**

```
输入：nums = [-7,-3,2,3,11]
输出：[4,9,9,49,121]
```

**提示：**

- 1 <= nums.length <= 104
- -104 <= nums[i] <= 104
- nums 已按 非递减顺序 排序

**进阶：**

- 请你设计时间复杂度为 O(n) 的算法解决本问题

#### 题解1

```C#
public class Solution
{
    public int[] SortedSquares(int[] nums)
    {
        // 将数组分为正数和负数两个部分，将两个指针分别指向边界两边，
        // 每次比较两指针对应的数，将较小的数放入答案并移动指针
        var boundary = -1;
        // 找到边界索引
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] < 0)
            {
                boundary = i;
            }
            else
            {
                break;
            }
        }
        var ans = new int[nums.Length];
        var leftPointer = boundary;
        var rightPointer = boundary + 1;
        var index = 0;
        // 只要两指针有一个没有超出数组边界，就继续
        while (leftPointer >= 0 || rightPointer < nums.Length)
        {
            // 右指针没超出数组边界，左指针已超出边界，取右指针对应的值
            if (leftPointer < 0)
            {
                ans[index] = nums[rightPointer] * nums[rightPointer];
                ++rightPointer;
            }
            // 左指针没超出数组边界，右指针已超出边界，取左指针对应的值
            else if (rightPointer > nums.Length - 1)
            {
                ans[index] = nums[leftPointer] * nums[leftPointer];
                --leftPointer;
            }
            // 两指针都没超出数组边界时，选择对应值较小的一个放入答案数组
            else if (nums[leftPointer] * nums[leftPointer] < nums[rightPointer] * nums[rightPointer])
            {
                ans[index] = nums[leftPointer] * nums[leftPointer];
                --leftPointer;
            }
            else
            {
                ans[index] = nums[rightPointer] * nums[rightPointer];
                ++rightPointer;
            }
            ++index;
        }
        return ans;
    }
}
```

#### 复杂度分析1

- 时间复杂度：O(n)，其中 n 是数组 nums 的长度。

- 空间复杂度：O(1)。除了存储答案的数组以外，我们只需要维护常量空间。

#### 题解2

```c#
public class Solution
{
    public int[] SortedSquares(int[] nums)
    {
        // 我们可以使用两个指针分别指向位置 0 和 n−1，每次比较两个指针对应的数，
        // 选择较大的那个逆序放入答案并移动指针。这种方法无需处理某一指针移动至边界的情况。
        var ans = new int[nums.Length];
        var leftPointer = 0;
        var rightPointer = nums.Length - 1;
        var index = nums.Length - 1;
        while (leftPointer <= rightPointer)
        {
            var left = nums[leftPointer] * nums[leftPointer];
            var right = nums[rightPointer] * nums[rightPointer];
            if (left < right)
            {
                ans[index] = right;
                --rightPointer;
            }
            else
            {
                ans[index] = left;
                ++leftPointer;
            }
            --index;
        }
        return ans;
    }
}
```

#### 复杂度分析2

- 时间复杂度：O(n)，其中 n 是数组 nums 的长度。

- 空间复杂度：O(1)。除了存储答案的数组以外，我们只需要维护常量空间。

### [189. 轮转数组](https://leetcode-cn.com/problems/rotate-array/)

#### 题目

给你一个数组，将数组中的元素向右轮转 k 个位置，其中 k 是非负数。

**示例 1:**

```
输入: nums = [1,2,3,4,5,6,7], k = 3
输出: [5,6,7,1,2,3,4]
解释:
向右轮转 1 步: [7,1,2,3,4,5,6]
向右轮转 2 步: [6,7,1,2,3,4,5]
向右轮转 3 步: [5,6,7,1,2,3,4]
```

**示例 2:**

```
输入：nums = [-1,-100,3,99], k = 2
输出：[3,99,-1,-100]
解释: 
向右轮转 1 步: [99,-1,-100,3]
向右轮转 2 步: [3,99,-1,-100]
```

**提示：**

- 1 <= nums.length <= 10^5^
- -2^31^ <= nums[i] <= 2^31^ - 1
- 0 <= k <= 10^5^

**进阶：**

- 尽可能想出更多的解决方案，至少有 三种 不同的方法可以解决这个问题。
- 你可以使用空间复杂度为 O(1) 的 原地 算法解决这个问题吗？

#### 题解

```C#
public class Solution
{
    public void Rotate(int[] nums, int k)
    {
        // 我们可以先将所有元素翻转，这样尾部的 k 个元素就被移至数组头部，
        // 然后我们再翻转 [0, k - 1] 区间的元素和 [k, n-1] 区间的元素即能得到最后的答案。
        k %= nums.Length;
        Reverse(nums, 0, nums.Length - 1);
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, nums.Length - 1);
    }

    public void Reverse(int[] nums, int start, int end)
    {
        while (start < end)
        {
            int temp = nums[start];
            nums[start] = nums[end];
            nums[end] = temp;
            start += 1;
            end -= 1;
        }
    }
}
```

#### 复杂度分析

时间复杂度：O(n)，其中 n 为数组的长度。每个元素被翻转两次，一共 n 个元素，因此总时间复杂度为 O(2n)=O(n)。

空间复杂度：O(1)。

## 第 3 天 双指针

### [283. 移动零](https://leetcode-cn.com/problems/move-zeroes/)

#### 题目

给定一个数组 nums，编写一个函数将所有 0 移动到数组的末尾，同时保持非零元素的相对顺序。

请注意 ，必须在不复制数组的情况下原地对数组进行操作。

**示例 1:**

```
输入: nums = [0,1,0,3,12]
输出: [1,3,12,0,0]
```

**示例 2:**

```
输入: nums = [0]
输出: [0]
```

**提示:**

- 1 <= nums.length <= 10^4^

- -2^31^ <= nums[i] <= 2^31^ - 1

**进阶：**

你能尽量减少完成的操作次数吗？

#### 题解

```C#
public class Solution
{
    public void MoveZeroes(int[] nums)
    {
        // 使用双指针，左指针指向当前已经处理好的序列的尾部，右指针指向待处理序列的头部。
        // 右指针不断向右移动，每次右指针指向非零数，则将左右指针对应的数交换，同时左指针右移。
        // 注意到以下性质：
        // 左指针左边均为非零数；
        // 右指针左边直到左指针处均为零。
        // 因此每次交换，都是将左指针的零与右指针的非零数交换，且非零数的相对顺序并未改变。
        var left = 0;
        var right = 0;
        while (right < nums.Length)
        {
            if (nums[right] != 0)
            {
                (nums[right], nums[left]) = (nums[left], nums[right]);
                ++left;
            }
            ++right;
        }
    }
}
```

#### 复杂度分析

- 时间复杂度：O(n)，其中 n 为序列长度。每个位置至多被遍历两次。
- 空间复杂度：O(1)。只需要常数的空间存放若干变量。
