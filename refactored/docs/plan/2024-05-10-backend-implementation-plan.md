# 后端开发实施计划

> **For agentic workers:** 使用本计划进行任务分解和跟踪。每个任务使用checkbox语法 `- [ ]` 标记进度。

**Goal:** 从零搭建完整的图书管理系统后端，包含用户认证、图书管理、借阅归还、逾期罚款等核心功能

**Architecture:** 采用FastAPI + SQLAlchemy 2.0 + PostgreSQL架构，分层设计（API/Service/Repository/Model），支持异步操作和并发安全

**Tech Stack:** Python 3.12+, FastAPI 0.110+, SQLAlchemy 2.0+, PostgreSQL 16, Alembic, JWT, bcrypt

---

## Phase 1: 项目基础搭建

### Task 1: 项目配置与环境

**Files:**
- Create: `refactored/src/app/config.py`
- Create: `refactored/src/app/main.py`
- Modify: `refactored/pyproject.toml`
- Create: `refactored/.env`

- [ ] **Step 1: 完善 pyproject.toml 依赖**

```toml
[project]
name = "library-system"
version = "1.0.0"
description = "图书管理系统 Python 重构版"
requires-python = ">=3.12"
dependencies = [
    "fastapi>=0.110.0",
    "uvicorn[standard]>=0.29.0",
    "sqlalchemy[asyncio]>=2.0.0",
    "asyncpg>=0.29.0",
    "pydantic>=2.5.0",
    "pydantic-settings>=2.0.0",
    "python-jose[cryptography]>=3.3.0",
    "bcrypt>=4.1.0",
    "python-multipart>=0.0.9",
    "passlib[bcrypt]>=1.7.4",
    "python-dotenv>=1.0.0",
    "alembic>=1.13.0",
    "redis>=5.0.0",
]

[project.optional-dependencies]
dev = [
    "pytest>=8.0.0",
    "pytest-asyncio>=0.23.0",
    "httpx>=0.27.0",
    "pytest-cov>=5.0.0",
    "ruff>=0.3.0",
    "mypy>=1.9.0",
]
```

- [ ] **Step 2: 创建配置类 config.py**

```python
from pydantic_settings import BaseSettings
from functools import lru_cache

class Settings(BaseSettings):
    app_name: str = "Library System"
    debug: bool = False
    database_url: str = "postgresql+asyncpg://user:password@localhost:5432/library"
    jwt_secret_key: str = "your-secret-key"
    jwt_algorithm: str = "HS256"
    access_token_expire_minutes: int = 120
    refresh_token_expire_days: int = 7
    redis_url: str = "redis://localhost:6379/0"
    
    class Config:
        env_file = ".env"

@lru_cache
def get_settings() -> Settings:
    return Settings()
```

- [ ] **Step 3: 创建应用入口 main.py**

```python
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from app.config import get_settings

settings = get_settings()

app = FastAPI(title=settings.app_name, version="1.0.0", debug=settings.debug)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.get("/health")
async def health_check():
    return {"status": "ok"}
```

- [ ] **Step 4: Commit**

```bash
git add refactored/pyproject.toml refactored/src/app/config.py refactored/src/app/main.py
git commit -m "feat: 项目基础配置和依赖"
```

---

## Phase 2: 核心模型定义

### Task 2: 用户模型

**Files:**
- Create: `refactored/src/app/models/user.py`

```python
import uuid
from sqlalchemy import Column, String, Boolean, DateTime, Integer
from sqlalchemy.dialects.postgresql import UUID
from app.db.base import Base, TimestampMixin

class User(Base, TimestampMixin):
    __tablename__ = "users"
    
    id = Column(UUID(as_uuid=True), primary_key=True, default=uuid.uuid4)
    username = Column(String(50), unique=True, nullable=False)
    email = Column(String(100), unique=True, nullable=False)
    password_hash = Column(String(255), nullable=False)
    role = Column(String(20), default="reader")
    status = Column(String(20), default="active")
    max_books = Column(Integer, default=0)
```

---

## 开发里程碑

| 阶段 | 任务 | 产出 | 预计时间 |
|------|------|------|----------|
| Phase 1 | 项目基础 | 配置、数据库连接 | 2h |
| Phase 2 | 模型定义 | 5个核心模型 | 3h |
| Phase 3 | 认证模块 | JWT登录 | 2h |
| Phase 4 | 服务层 | Repository+Service | 4h |
| Phase 5 | 核心业务 | 借书/还书 | 4h |
| Phase 6 | API层 | 路由定义 | 3h |
| Phase 7 | 测试部署 | 测试+Docker | 2h |

**总计**: 约20小时开发工作量

---

## 执行建议

1. **按Phase顺序执行**，每个Phase完成后可独立运行测试
2. **每个Task独立提交**，便于回滚和代码审查
3. **遇到阻塞立即反馈**，调整计划
4. **完成后执行**: `docker-compose up` 验证整体功能
