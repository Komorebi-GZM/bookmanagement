FROM python:3.12-slim

WORKDIR /app

# 安装系统依赖
RUN apt-get update && apt-get install -y \
    gcc \
    libpq-dev \
    && rm -rf /var/lib/apt/lists/*

# 复制依赖文件
COPY pyproject.toml .

# 安装Python依赖
RUN pip install --no-cache-dir -e .

# 复制应用代码
COPY src/ ./src/

# 暴露端口
EXPOSE 8000

# 启动命令
CMD ["uvicorn", "src.app.main:app", "--host", "0.0.0.0", "--port", "8000"]
