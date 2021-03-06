﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBHelper.Model
{



    /// <summary>
    /// 题目类型
    /// </summary>
    public struct TitleType
    {
        public int TiteTypeId { get; set; }
        public string TitleTypeName { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    /// <summary>
    /// 题目与选项关系表
    /// </summary>
    public struct TitleItemAssoc
    {
        public int TitleItemAssocId { get; set; }
        public int TitleInfoId { get; set; }
        public int TitleItemId { get; set; }
        /// <summary>
        /// 选择项 在题目中的索引（该项在题目中的显示位置,答案填写的值）
        /// </summary>
        public int TitleItemIndex { get; set; }
        public string TitleItemContent { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }

    /// <summary>
    /// 题目选项
    /// </summary>
    public struct TitleItem
    {
        public int TitleItemId { get; set; }
        public string TitleItemContent { get; set; }
        public DateTime UpdateDateTime { get; set; }
//         #region ************  其他 
//         /// <summary>
//         /// 选择项 在题目中的索引（该项在题目中的显示位置,答案填写的值）
//         /// </summary>
//         public int TitleItemIndex { get; set; }
// 
//         #endregion
    }

    /// <summary>
    /// 题目信息
    /// </summary>
    public struct TitleInfo
    {
        public int TitleInfoId { get; set; }
        public string TitleConent { get; set; }
        /// <summary>
        /// 题目类型(选择或判断)
        /// </summary>
        public int TitleTypeId { get; set; }
        /// <summary>
        /// 题目类别()
        /// </summary>
        public int TypeId { get; set; }
        public int CorrectAnswer { get; set; }
        public double Score { get; set; }
        public DateTime UpdateDateTime { get; set; }
        /// <summary>
        /// 题目的选项列表
        /// </summary>
        public List<TitleItem> ListTitleItem { get; set; }

        #region ************* 其他
        /// <summary>
        /// 题目类型名称
        /// </summary>
        public string TitleTypeName { get; set; }
        /// <summary>
        /// 大类实验名称
        /// </summary>
        public string TypeName1 { get; set; }
        /// <summary>
        /// 小类实验名称
        /// </summary>
        public string TypeName2 { get; set; }
        #endregion

    }

    /// <summary>
    /// 学生考试答题详情
    /// </summary>
    public struct StudentExaminationPaper
    {
        public int StudentExaminationPaperId { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 题目编号
        /// </summary>
        public int TitleInfoId { get; set; }
        /// <summary>
        /// 学生写的答案
        /// </summary>
        public int StudentAnswer { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 考试Id
        /// </summary>
        public int ExercisesTestId { get; set; }
    }

    public struct StudentExam
    {
        /// <summary>
        /// 
        /// </summary>
        public int StudentExamId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int StudentSex { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string StudentPhone { get; set; }
        /// <summary>
        /// 是否是第一次登录 
        /// </summary>
        public int IsFirstLogin{ get; set; }
        /// <summary>
        /// 最后一次登录 时间
        /// </summary>
        public string LoginDateTime { get; set; }
        /// <summary>
        /// 角色类型(学生或教师)
        /// </summary>
        public int Type { get; set; }

        public string Password { get; set; }

        #region ************  其他
        /// <summary>
        /// 总分
        /// </summary>
        public double TotalScore;

        #endregion
    }

    /// <summary>
    /// 实验类型
    /// </summary>
    public struct ExperimentType
    {
        public int TypeId { get; set; }
        /// <summary>
        /// 基础认知等
        /// </summary>
        public string TypeName1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName2 { get; set; }
        public string TypeDescribe { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }
    }
    /// <summary>
    /// 练习、考试与题目对照表
    /// </summary>
    public struct ExercisesTitle
    {
        public int ExercisesTitleId { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public int TitleInfoId { get; set; }
        /// <summary>
        /// 考试ID
        /// </summary>
        public int ExercisesTestId { get; set; }

        public int ExercisesTitleIndex { get; set; }

        public DateTime UpdateDateTime { get; set; }

        #region ******************** 其他
        /// <summary>
        /// 题目
        /// </summary>
        public string TitleConent { get; set; }
        /// <summary>
        /// 正确答案(判断题 1正确,2错误 )
        /// </summary>
        public int CorrectAnswer { get; set; }
        /// <summary>
        /// 该题得分
        /// </summary>
        public int Score { get; set; }

        ///// <summary>
        ///// 考试的实验类型
        ///// </summary>
        //public int TypeId { get; set; }
        ///// <summary>
        ///// 考试的实验类型名称
        ///// </summary>
        //public int TypeName1 { get; set; }
        ///// <summary>
        ///// 考试的实验类型名称
        ///// </summary>
        //public int TypeName2 { get; set; }
        #endregion

    }

    

    /// <summary>
    /// 习题或考试表
    /// </summary>
    public struct ExercisesTest
    {
        public int ExercisesTestId { get; set; }
        /// <summary>
        /// 考试名称
        /// </summary>
        public string ExercisesName { get; set; }
        /// <summary>
        /// 考试描述
        /// </summary>
        public string ExercisesDescribe { get; set; }
        /// <summary>
        /// -1:表示一次考试。其它表示一次习题，与ExperimentType表中的TypeId相关联
        /// </summary>
        public int ExercisesTypeId { get; set; }

        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        /// 考试是否结束
        /// </summary>
        public int IsOver { get; set; }


        #region **************  其他

        /// <summary>
        /// 是否使用该考试(前台显示的考试)
        /// </summary>
        public int IsUse { get; set; }

        /// <summary>
        /// 一次考试对应的题目列表
        /// </summary>
        public List<TitleInfo> ListTitleInfo { get; set; }

        /// <summary>
        /// 基础认知等
        /// </summary>
        public string TypeName1 { get; set; }
        public string TypeName2 { get; set; }
        #endregion

    }


    public struct CurrentExercises
    {
        public int CurrentExercisesId { get; set; }
        /// <summary>
        /// 前台显示的考试()
        /// </summary>
        public int ExercisesTestId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int ExercisesTypeId { get; set; }
    }

    public struct StudentExamState
    {
        /// <summary>
        /// 学生考试 状态ID
        /// </summary>
        public int StudentExamStateId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 考试ID
        /// </summary>
        public int ExercisesTestId { get; set; }
        /// <summary>
        /// 状态(0 可继续答题,1 已交卷不可再答题)
        /// </summary>
        public int State { get; set; }
    }

    public struct LoginInfo
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }

        public string NewPassword { get; set; }
    }


    

    public struct LoginResult
    {
        /// <summary>
        /// 学号或教师工号
        /// </summary>
        public string UserID;
        /// <summary>
        /// 学号或教师姓名
        /// </summary>
        public string UserName;
        /// <summary>
        /// 1 是第一次登录需要修改密码,其他不需要修改密码
        /// </summary>
        public int IsFirstLogin;

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string LoginDateTime;
    }


    #region **********************************************  考试 前台传到后台参数
    public struct stGetExperimentType
    {
        /// <summary>
        /// 当前第几页
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页显示个数
        /// </summary>
        public int ShowCount { get; set; }
    }

    public struct stringId
    {
        public string strId { get; set; }
    }

    public struct stId
    {
        public int Id { get; set; }
    }
    public struct stId2
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }
    }

    public struct ExamList
    {

        /// <summary>
        /// 考试或练习ID
        /// </summary>
        public int ExercisesTestId { get; set; }
        /// <summary>
        /// 考试或练习名称
        /// </summary>
        public string ExercisesName { get; set; }
        /// <summary>
        /// 考试或练习描述
        /// </summary>
        public string ExercisesDescribe { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        //public int StudentExamId { get; set; }
        /// <summary>
        /// 学生得分
        /// </summary>
        public double StudentScore { get; set; }
        /// <summary>
        /// 考试总分
        /// </summary>
        public double TotleScore { get; set; }

    }


    public struct ExamItemInfo
    {
        /// <summary>
        /// 题目选项ID
        /// </summary>
        public int ExamItemId { get; set; }
        /// <summary>
        /// 题目选项名称
        /// </summary>
        public string ExamItemName { get; set; }
        /// <summary>
        /// 该选项在题目中的位置(题目答案与此项相关)
        /// </summary>
        public int TitleItemIndex { get; set; }

    }

    /// <summary>
    /// 考试题目
    /// </summary>
    public struct ExamTitleInfo
    {
        /// <summary>
        /// 题目ID
        /// </summary>
        public int TitleInfoId { get; set; }
        /// <summary>
        /// 题目内容 
        /// </summary>
        public string TitleConent { get; set; }

        /// <summary>
        /// 正确答案 
        /// </summary>
        public int CorrectAnswer { get; set; }
        /// <summary>
        /// 学生答案
        /// </summary>
        public int StudentAnswer { get; set; }

        /// <summary>
        /// 该题分数
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// 题目类型名称(选择或判断)
        /// </summary>
        public string TitleTypeName { get; set; }

        /// <summary>
        /// 题目选项(已按照选项顺序位置排好,前台直接按索引取)
        /// </summary>
        public List<ExamItemInfo> ListExamItem { get; set; }
    }

    public struct ExamInfo
    {
        /// <summary>
        /// 考试或练习ID
        /// </summary>
        public int ExercisesTestId { get; set; }
        /// <summary>
        /// 考试或练习名称
        /// </summary>
        public string ExercisesName { get; set; }
        /// <summary>
        /// 考试或练习描述
        /// </summary>
        public string ExercisesDescribe { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        public int StudentExamId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学生学号
        /// </summary>
        public string StudentNumber { get; set; }
        /// <summary>
        /// 学生得分
        /// </summary>
        public double StudentScore { get; set; }
        /// <summary>
        /// 考试总分
        /// </summary>
        public double TotleScore { get; set; }

        /// <summary>
        /// 考试是否结束
        /// </summary>
        public int IsOver { get; set; }

        /// <summary>
        /// 学生是否交卷(0 未交,1 已交)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 题目列表(按照题目顺序排好顺序)
        /// </summary>
        public List<ExamTitleInfo> ListExamTitle { get; set; }
    }

    /// <summary>
    /// 学生做题的每一个小题
    /// </summary>
    public struct StudentItemAnswer
    {
        /// <summary>
        /// 考试ID
        /// </summary>
        public int ExercisesTestId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentExamId { get; set; }
        /// <summary>
        /// 题目ID
        /// </summary>
        public int TitleInfoId { get; set; }
        /// <summary>
        /// 学生答案
        /// </summary>
        public int StudentAnswer { get; set; }
    }

    #endregion


    /// <summary>
    /// 报告信息
    /// </summary>
    public struct WebReportInfo
    {
        public int WebReportId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 实验项目名称
        /// </summary>
        public string ExperimentName { get; set; }
        /// <summary>
        /// 开课学院及实验室
        /// </summary>
        public string ExperimentAddress { get; set; }
        /// <summary>
        /// 实验日期
        /// </summary>
        public string ExperimentDate { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNumber { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 老师姓名
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public double Score { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string Title4 { get; set; }
        public string Title5 { get; set; }
        public string Title6 { get; set; }
        public string Title7 { get; set; }
    }

    /// <summary>
    /// 试用信息
    /// </summary>
    public class PeriodInfo
    {
        public int PeriodId { get; set; }
        /// <summary>
        /// 硬盘号
        /// </summary>
        public string PeriodNumber { get; set; }
        /// <summary>
        /// 开始试用时间
        /// </summary>
        public string StartDateTime { get; set; }
        /// <summary>
        /// 试用天数(暂未使用,通过试用总控来控制试用天数)
        /// </summary>
        public int PeriodDay { get; set; }
        /// <summary>
        /// 试用别名
        /// </summary>
        public string PeriodAlias { get; set; }

    }


    /// <summary>
    /// 试用总控
    /// </summary>
    public class PeriodTotalInfo
    {
        public int PeriodTotalId { get; set; }
        /// <summary>
        /// 试用天数
        /// </summary>
        public int PeriodDay { get; set; }
        /// <summary>
        /// 是否试用
        /// </summary>
        public bool PeriodBool { get; set; }
    }
}

