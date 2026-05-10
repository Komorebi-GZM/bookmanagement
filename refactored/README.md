# Library System

图书管理系统 Python 重构版（FastAPI + Vue3 + PostgreSQL）

## 目录结构

```
library-system/
├── docs/           # 全套工程文档
├── src/            # 后端源码
├── web/            # 前端源码
├── deploy/         # 部署配置
├── .env.example    # 环境变量模板
├── pyproject.toml # 依赖管理（PEP 621）
└── README.md       # 本文档
```

## 快速启动

```bash
# 1. 复制环境变量
cp .env.example .env
# 编辑 .env 填写真实配置

# 2. 启动数据库
docker compose -f deploy/docker-compose.yml up -d postgres redis

# 3. 启动后端
cd src && uvicorn app.main:app --reload

# 4. 启动前端
cd web && npm install && npm run dev
```

## 文档索引

| 文档 | 说明 |
|------|------|
| docs/00_项目总览.md | 定位、目标、重构问题 |
| docs/01_技术规格书总纲.md | 架构、选型、规范 |
| docs/02_数据模型设计.md | 表结构、状态机、约束 |
| docs/03_API接口设计规范.md | 端点规划、认证、错误码 |
| docs/04_后端架构设计.md | 分层、模块、事务控制 |
| docs/05_前端路由与页面.md | 路由规划、页面权限 |
| docs/06_状态机业务流程.md | 借书/还书/逾期全流程 |
| docs/07_权限设计.md | 角色、资源、细粒度控制 |
| docs/08_并发安全设计.md | 事务、锁、幂等设计 |
| docs/09_部署与运维规范.md | Docker、环境、监控 |
| docs/10_测试计划.md | 测试范围、用例、验收标准 |
| docs/11_迭代交付规划.md | 三期节奏、里程碑 |
| docs/12_工程化规范.md | 代码、提交、命名规范 |
