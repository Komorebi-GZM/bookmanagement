# 03 API接口设计规范

## 1. 设计原则

### 1.1 RESTful设计规范

| 原则 | 说明 | 示例 |
|------|------|------|
| **资源导向** | URL表示资源，使用名词复数 | `/api/v1/books`, `/api/v1/users` |
| **HTTP方法** | 使用标准HTTP方法表示操作 | GET/POST/PUT/DELETE/PATCH |
| **状态码** | 使用标准HTTP状态码 | 200/201/204/400/401/403/404/500 |
| **无状态** | 每个请求独立，服务端不保存会话 | JWT Token认证 |
| **版本控制** | URL中包含API版本 | `/api/v1/...` |

### 1.2 HTTP方法语义

| 方法 | 用途 | 幂等性 | 示例 |
|------|------|--------|------|
| GET | 获取资源 | 是 | `GET /api/v1/books` |
| POST | 创建资源 | 否 | `POST /api/v1/books` |
| PUT | 全量更新 | 是 | `PUT /api/v1/books/{id}` |
| PATCH | 部分更新 | 否 | `PATCH /api/v1/books/{id}` |
| DELETE | 删除资源 | 是 | `DELETE /api/v1/books/{id}` |

---

## 2. 接口分组

### 2.1 接口分组概览

```
/api/v1
├── /auth          # 认证接口
├── /users         # 用户接口
├── /books         # 图书接口
├── /categories    # 分类接口
├── /borrow        # 借阅接口
├── /rules         # 规则接口
└── /logs          # 日志接口
```

### 2.2 分组详细说明

| 分组 | 路径前缀 | 说明 | 访问角色 |
|------|----------|------|----------|
| 认证 | `/api/v1/auth` | 登录、登出、刷新Token | 公开 |
| 用户 | `/api/v1/users` | 用户CRUD、个人信息 | admin/librarian/reader |
| 图书 | `/api/v1/books` | 图书CRUD、库存管理 | admin/librarian/reader(读) |
| 分类 | `/api/v1/categories` | 分类CRUD、树形结构 | admin/librarian(写)/reader(读) |
| 借阅 | `/api/v1/borrow` | 借书、还书、续借、逾期 | admin/librarian/reader |
| 规则 | `/api/v1/rules` | 借阅规则、罚款规则 | admin/librarian(读) |
| 日志 | `/api/v1/logs` | 操作日志查询 | admin |

---

## 3. 统一响应格式

### 3.1 成功响应

```json
{ "code": 200, "message": "success", "data": { ... }, "timestamp": "..." }
```

### 3.2 列表响应（分页）

```json
{
  "code": 200, "message": "success",
  "data": {
    "items": [ ... ],
    "pagination": { "page": 1, "page_size": 20, "total": 100, "total_pages": 5, "has_next": true, "has_prev": false }
  }
}
```

### 3.3 错误响应

```json
{
  "code": 400, "message": "请求参数错误",
  "error": { "type": "ValidationError", "detail": "ISBN格式不正确", "field": "isbn" }
}
```

---

## 4. 错误码规范

### 4.1 HTTP状态码映射

| HTTP状态码 | 含义 | 使用场景 |
|------------|------|----------|
| 200 | OK | 请求成功 |
| 201 | Created | 资源创建成功 |
| 204 | No Content | 删除成功，无返回内容 |
| 400 | Bad Request | 请求参数错误 |
| 401 | Unauthorized | 未认证或Token失效 |
| 403 | Forbidden | 无权限访问 |
| 404 | Not Found | 资源不存在 |
| 409 | Conflict | 资源冲突（如重复借阅） |
| 422 | Unprocessable Entity | 业务逻辑错误 |
| 429 | Too Many Requests | 请求频率限制 |
| 500 | Internal Server Error | 服务器内部错误 |

### 4.2 业务错误码

| 错误码 | 错误类型 | 说明 |
|--------|----------|------|
| 1001 | AUTH_INVALID_CREDENTIALS | 用户名或密码错误 |
| 1002 | AUTH_TOKEN_EXPIRED | Token已过期 |
| 1003 | AUTH_TOKEN_INVALID | Token无效 |
| 1004 | AUTH_ACCESS_DENIED | 访问被拒绝 |
| 2001 | USER_NOT_FOUND | 用户不存在 |
| 2002 | USER_ALREADY_EXISTS | 用户已存在 |
| 2003 | USER_FROZEN | 用户已冻结 |
| 2004 | USER_BLACKLISTED | 用户已被列入黑名单 |
| 3001 | BOOK_NOT_FOUND | 图书不存在 |
| 3002 | BOOK_NOT_AVAILABLE | 图书不可借 |
| 3003 | BOOK_OUT_OF_STOCK | 图书库存不足 |
| 3004 | BOOK_ALREADY_BORROWED | 图书已被该用户借阅 |
| 4001 | BORROW_QUOTA_EXCEEDED | 借阅数量超限 |
| 4002 | BORROW_NOT_FOUND | 借阅记录不存在 |
| 4003 | BORROW_ALREADY_RETURNED | 图书已归还 |
| 4004 | BORROW_OVERDUE | 图书已逾期 |
| 4005 | BORROW_RENEW_LIMIT | 续借次数已达上限 |
| 5001 | CATEGORY_NOT_FOUND | 分类不存在 |
| 5002 | CATEGORY_HAS_BOOKS | 分类下存在图书，无法删除 |
| 6001 | FINE_UNPAID | 有未缴纳罚款 |
| 6002 | FINE_NOT_FOUND | 罚款记录不存在 |

---

## 5. JWT认证流程

### 5.1 Token结构设计

#### Access Token

```json
{ "sub": "user_uuid", "username": "zhangsan", "role": "reader", "iat": 1705312200, "exp": 1705315800, "type": "access" }
```

#### Refresh Token

```json
{ "sub": "user_uuid", "iat": 1705312200, "exp": 1707899400, "type": "refresh" }
```

### 5.2 认证流程

```
┌─────────┐                              ┌─────────┐
│  Client │                              │ Server  │
└────┬────┘                              └────┬────┘
     │                                        │
     │  1. POST /api/v1/auth/login            │
     │     {username, password}               │
     │───────────────────────────────────────►│
     │                                        │
     │  2. 返回 Access Token + Refresh Token  │
     │◄───────────────────────────────────────│
     │                                        │
     │  3. 后续请求携带 Access Token          │
     │     Authorization: Bearer {token}      │
     │───────────────────────────────────────►│
     │                                        │
     │  4. 返回请求数据                       │
     │◄───────────────────────────────────────│
     │                                        │
     │  5. Access Token 过期 (401)            │
     │◄───────────────────────────────────────│
     │                                        │
     │  6. POST /api/v1/auth/refresh          │
     │     {refresh_token}                    │
     │───────────────────────────────────────►│
     │                                        │
     │  7. 返回新的 Access Token              │
     │◄───────────────────────────────────────│
```

### 5.3 Token配置

| 配置项 | 值 | 说明 |
|--------|-----|------|
| Access Token 有效期 | 1小时 | 短期有效，减少泄露风险 |
| Refresh Token 有效期 | 30天 | 长期有效，用于刷新 |
| Token 算法 | HS256 | HMAC SHA-256 |
| Token 签发者 | "bookms" | 系统标识 |

---

## 6. 权限校验机制

### 6.1 角色定义

| 角色 | 标识 | 权限范围 |
|------|------|----------|
| 系统管理员 | admin | 全部权限 |
| 图书管理员 | librarian | 图书管理、借阅管理、用户查看 |
| 读者 | reader | 图书查询、个人借阅、个人信息 |

### 6.2 权限矩阵

| 功能 | admin | librarian | reader |
|------|-------|-----------|--------|
| 用户管理(CRUD) | 全部 | 查看 | 仅自己 |
| 图书管理(CRUD) | 全部 | 全部 | 查看 |
| 分类管理(CRUD) | 全部 | 全部 | 查看 |
| 借阅操作 | 全部 | 全部 | 仅自己 |
| 规则配置 | 全部 | 查看 | - |
| 日志查看 | 全部 | - | - |
| 罚款处理 | 全部 | 全部 | 仅自己 |

### 6.3 权限校验设计

- 使用 FastAPI 依赖注入实现 `require_role(*roles)` 装饰器
- 使用 `require_owner_or_role(user_id, *roles)` 实现资源所有者或指定角色校验
- 路由级别通过 `Depends(require_role(...))` 声明所需权限

---

## 7. 接口详细说明

### 7.1 认证接口 (/api/v1/auth)

| 端点 | 方法 | 说明 | 权限 |
|------|------|------|------|
| `/api/v1/auth/login` | POST | 用户登录 | 公开 |
| `/api/v1/auth/refresh` | POST | 刷新Token | 公开 |
| `/api/v1/auth/logout` | POST | 用户登出 | 认证 |

**登录错误码:** 1001(用户名或密码错误), 2003(用户已冻结), 2004(用户已被列入黑名单)

---

### 7.2 用户接口 (/api/v1/users)

| 端点 | 方法 | 说明 | 权限 |
|------|------|------|------|
| `/api/v1/users` | GET | 获取用户列表(分页、角色/状态筛选、关键词搜索) | admin, librarian |
| `/api/v1/users` | POST | 创建用户 | admin |
| `/api/v1/users/{id}` | GET | 获取用户详情 | admin, librarian, reader(仅自己) |
| `/api/v1/users/{id}` | PUT | 更新用户 | admin(全部), reader(仅自己) |
| `/api/v1/users/{id}` | DELETE | 删除用户 | admin |
| `/api/v1/users/{id}/change-password` | POST | 修改密码 | admin(任意), reader/librarian(仅自己) |
| `/api/v1/users/{id}/status` | PATCH | 冻结/解冻用户 | admin |

---

### 7.3 图书接口 (/api/v1/books)

| 端点 | 方法 | 说明 | 权限 |
|------|------|------|------|
| `/api/v1/books` | GET | 获取图书列表(分页、分类/状态筛选、关键词搜索) | 认证 |
| `/api/v1/books` | POST | 创建图书 | admin, librarian |
| `/api/v1/books/{id}` | GET | 获取图书详情 | 认证 |
| `/api/v1/books/{id}` | PUT | 更新图书 | admin, librarian |
| `/api/v1/books/{id}` | DELETE | 删除图书 | admin, librarian |
| `/api/v1/books/{id}/inventory` | POST | 调整库存(increase/decrease/set) | admin, librarian |

---

### 7.4 分类接口 (/api/v1/categories)

| 端点 | 方法 | 说明 | 权限 |
|------|------|------|------|
| `/api/v1/categories` | GET | 获取分类列表(支持tree=true返回树形结构) | 认证 |
| `/api/v1/categories` | POST | 创建分类 | admin, librarian |
| `/api/v1/categories/{id}` | PUT | 更新分类 | admin, librarian |
| `/api/v1/categories/{id}` | DELETE | 删除分类(分类下有图书时不可删除) | admin, librarian |

---

### 7.5 借阅接口 (/api/v1/borrow)

| 端点 | 方法 | 说明 | 权限 |
|------|------|------|------|
| `/api/v1/borrow` | GET | 获取借阅记录列表(支持用户/图书/状态筛选、仅显示逾期) | admin, librarian(全部), reader(仅自己) |
| `/api/v1/borrow` | POST | 借阅图书 | admin, librarian |
| `/api/v1/borrow/{id}/return` | POST | 归还图书 | admin, librarian |
| `/api/v1/borrow/{id}/renew` | POST | 续借图书 | admin, librarian, reader(仅自己) |
| `/api/v1/borrow/statistics` | GET | 获取借阅统计 | admin, librarian |

**借阅错误码:** 3003(库存不足), 3004(已借阅), 4001(借阅超限), 6001(有未缴罚款)

**续借错误码:** 4004(已逾期不能续借), 4005(续借次数达上限)

---

### 7.6 规则接口 (/api/v1/rules)

| 端点 | 方法 | 说明 | 权限 |
|------|------|------|------|
| `/api/v1/rules/borrow` | GET | 获取借阅规则列表 | admin, librarian |
| `/api/v1/rules/borrow` | POST | 创建借阅规则 | admin |
| `/api/v1/rules/borrow/{id}` | PUT | 更新借阅规则 | admin |
| `/api/v1/rules/borrow/{id}` | DELETE | 删除借阅规则 | admin |
| `/api/v1/rules/borrow/my` | GET | 获取当前用户适用规则(含剩余配额) | 认证 |

---

### 7.7 日志接口 (/api/v1/logs)

| 端点 | 方法 | 说明 | 权限 |
|------|------|------|------|
| `/api/v1/logs/operation` | GET | 获取操作日志(支持用户/操作类型/日期范围筛选) | admin |
| `/api/v1/logs/login` | GET | 获取登录日志 | admin |

---

## 8. 请求/响应规范

### 8.1 请求规范

#### 请求头
```
Content-Type: application/json
Accept: application/json
Authorization: Bearer {access_token}
X-Request-ID: {uuid}  # 可选，用于链路追踪
```

#### 日期时间格式
- 统一使用 ISO 8601 格式: `2024-01-15T10:30:00Z`
- 时区统一使用 UTC

#### 空值处理
- 请求中可选字段为空时，不传或传 `null`
- 响应中空值统一返回 `null`，不返回空字符串

### 8.2 响应规范

#### 分页参数
| 参数 | 类型 | 说明 |
|------|------|------|
| page | integer | 当前页码 |
| page_size | integer | 每页数量 |
| total | integer | 总记录数 |
| total_pages | integer | 总页数 |
| has_next | boolean | 是否有下一页 |
| has_prev | boolean | 是否有上一页 |

#### 排序参数
```
GET /api/v1/books?sort=-created_at,+title
```
- `+` 表示升序 (可省略)
- `-` 表示降序

---

## 9. 接口安全

### 9.1 频率限制

| 接口类型 | 限制 |
|----------|------|
| 登录接口 | 5次/分钟 |
| 普通接口 | 100次/分钟 |
| 批量操作 | 10次/分钟 |

### 9.2 输入校验

| 数据类型 | 校验规则 |
|----------|----------|
| UUID | 标准UUID格式 |
| 邮箱 | RFC 5322 标准 |
| ISBN | ISBN-10 或 ISBN-13 |
| 日期 | ISO 8601 格式 |
| 分页参数 | page >= 1, page_size <= 100 |

---

## 10. 相关文档

- [02_数据模型设计.md](./02_数据模型设计.md)
- [04_后端架构设计.md](./04_后端架构设计.md)
- [07_权限设计.md](./07_权限设计.md)
