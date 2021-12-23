# 1 RabbitMQ简介

## 1.1 安装

### 1.1.1 docker方式

```bash
#15672端口为web管理端的端口，5672为RabbitMQ服务的端口
docker run -d  --name rabbitmq -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=123456 -p 15672:15672 -p 5672:5672 :3.9.11-management
```

### 1.1.2 docker-compose方式

```bash
version: '3'
services:
  rabbitmq:
    image: rabbitmq:3.9.11-management
    container_name: rabbitmq
    restart: always
    hostname: rabbitmq_host
    ports:
      - 15672:15672   # web UI 管理接口
      - 5672:5672	# 生产者和消费者连接使用的接口
    volumes:
      - ./log:/var/log/rabbitmq #挂载 RabbitMQ日志
      - ./data:/var/lib/rabbitmq #挂载 RabbitMQ数据
    environment:
      - RABBITMQ_DEFAULT_USER=admin
      - RABBITMQ_DEFAULT_PASS=123456
```

	### 1.1.3 参数说明

15672：UI端使用端口
5672：rabbitmq api 端口
浏览器访问 ip:15672即可，输入用户名，密码登录。

推荐为docker容器设置hostname，因为rabbitmq默认使用hostname作为存储数据的节点名，设置hostname可以避免生成随机的节点名，方便追踪数据。官网原文如下

> One of the important things to note about RabbitMQ is that it stores data based on what it calls the "Node Name", which defaults to the hostname. What this means for usage in Docker is that we should specify -h/--hostname explicitly for each daemon so that we don't get a random hostname and can keep track of our data:

RABBITMQ_DEFAULT_USER 和 RABBITMQ_DEFAULT_PASS
用来设置超级管理员的账号和密码，如果不设置，默认都是 guest

docker镜像使用像这样 rabbitmq:3.8.3-management 带有后缀 -management的镜像，之前使用没带这个后缀的镜像，网页访问失败

### 1.1.3 安装后的校验和准备

- 浏览器输入 localhost:15672，进入web管理界面
- 建一个名为develop的Virtual host(虚拟主机)使用，项目中一般是一个项目建一个Virtual host用，能够隔离队列
- ![image-20211223182906848](https://gitee.com/cyoukon/Resources/raw/master/images/20211223182913.png)

